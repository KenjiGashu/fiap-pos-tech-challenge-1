(ql:quickload :drakma)
(ql:quickload :cl-json)
(ql:quickload :shasht)

(setq *print-pretty* t)

;; (require 'sb-posix)
;; (sb-posix:setenv "FIAP_POS_PORT" "8080" 1)
(setf *port* "5129")

(defparameter *port* (sb-ext:posix-getenv "FIAP_POS_PORT"))
(defparameter *jwt-token* "your.jwt.token.here")
(defparameter *admin-email* "Admin@gmail.com")
(defparameter *admin-password* "1234")
(defparameter *api-url* (concatenate 'string "http://" *ip* ":" *port* "/api/"))
(defparameter *cliente-id* "")
(defparameter *ordem-servico-id* "")
(defparameter *comandos* "")
(defparameter *usuario-email* "")
(defparameter *usuario-id* "")
(defparameter *cliente-id* "")
(defparameter *veiculo-id* "")

(push '("application" . "json") drakma:*text-content-types*)

(defun get-token (json)
	(let ((token (cdr (assoc :token json))))
		(format t "gashu ~a~%" token)
		token))

(defun do-api-call (api body type)
	(let* ((url (concatenate 'string *api-url* api)))
		(format t "Doing ~A request to ~A~%" type url)
		(multiple-value-bind (body status-code headers uri stream must-close status-text)
				(drakma:http-request
				 url
				 :method type
				 :content body
				 :additional-headers
				 `(("Content-Type" . "application/json")
					 ("Authorization" . ,(format nil "Bearer ~A" *jwt-token*))))

			(format t "Status: ~A ~A~%" status-code status-text)
			(when body
				(format t "Response Body: ~A~%" (shasht:write-json (shasht:read-json body) nil)))
			body)))

(defun do-post (api body)
	(do-api-call api body :post))

(defun do-get (api body)
	(do-api-call api body :get))

(defun do-delete (api body)
	(do-api-call api body :delete))

(defun do-put (api body)
	(do-api-call api body :put))

(defun do-login (password email)
	(let* ((data (cl-json:encode-json-to-string `(("password" . ,password) ("email" . ,email))))
				 (url (concatenate 'string *api-url* "identidade/login"))
				 (response (drakma:http-request
										url
										:method :post
										:content data
										:additional-headers
										`(("Content-Type" . "application/json")))))
		(format t "response: ~A~%" response)
		
		(let* ((json-obj (cl-json:decode-json-from-string response))
					 (resp (get-token json-obj)))
			(setf *jwt-token* resp))))

(defun do-login-admin ()
	(do-login *admin-password* *admin-email*))

(defun do-login-cliente ()
	(do-login *usuario-password* *usuario-email*))

(defun get-all-usuarios ()
	(let* ((body (do-get "identidade/usuarios" nil)))
		(when body
			(format t "Response Body: ~A~%" (shasht:write-json (shasht:read-json body) nil)))
		body))

(defun cria-usuario (password email roles)
	(let* ((data (cl-json:encode-json-to-string `(("password" . ,password) ("email" . ,email) ("roles" . ,roles))))
				 (body (do-post "identidade/usuarios" data)))
		(format t "response: ~A~%" body)
		(setf *usuario-email* email)
		(setf *usuario-password* password)))

(defun seleciona-usuario ()
	(let* ((url (concatenate 'string "identidade/usuarios/" *usuario-email*))
				 (body (do-get url nil))
				 (json-obj (cl-json:decode-json-from-string body))
				 (usuario (cdr (assoc :data json-obj)))
				 (email (cdr (assoc :email usuario)))
				 (usuario-id (cdr (assoc :id usuario))))
		(setf *usuario-email* email)
		(setf *usuario-id* usuario-id)))


(defun cria-cliente (nome cpf cnpj tipopessoa)
	(let* ((data (cl-json:encode-json-to-string `(("Nome" . ,nome) ("UsuarioId" . ,*usuario-id*) ("Cpf" . ,cpf) ("Cnpj" . ,cnpj) ("TipoPessoa" . ,tipopessoa))))
				 (body (do-post "Cliente" data)))
		(setf *cliente-nome* nome)))

(defun seleciona-cliente ()
	(let* ((url (concatenate 'string "cliente/Nome/" *cliente-nome*))
				 (body (do-get url nil))
				 (json-obj (cl-json:decode-json-from-string body))
				 (id (cdr (assoc :id json-obj))))
		(setf *cliente-id* id)))

(defun cria-veiculo (placa marca modelo ano)
	(let* ((data (cl-json:encode-json-to-string `(("placa" . ,placa) ("marca" . ,marca) ("modelo" . ,modelo) ("ano" . ,ano))))
				 (body (do-post "veiculo" data)))
		(setf *veiculo-placa* placa)))

(defun seleciona-veiculo ()
	(let* ((url (concatenate 'string "veiculo/byPlaca/" *veiculo-placa*))
				 (body (do-get url nil))
				 (json-obj (cl-json:decode-json-from-string body))
				 (id (cdr (assoc :id json-obj))))
		(setf *veiculo-id* id)))

(defun cria-ordem-servico ()
	(let* ((data (cl-json:encode-json-to-string `(("ClienteId" . ,*cliente-id*) ("VeiculoId" . ,*veiculo-id*))))
				 (body (do-post "ordemservico" data)))
		body))

(defun all-ordem-servico ()
	(let* ((body (do-get "ordemServico" nil))
				 (json-obj (cl-json:decode-json-from-string body))
				 (id (cdr (assoc :id json-obj))))
		body))

(defun seleciona-ordem-servico ()
	(let* ((url (concatenate 'string "ordemservico/cliente/" *cliente-id*))
				 (body (do-get url nil))

				 (json-obj (cl-json:decode-json-from-string body))
				 (id (cdr (assoc :id (first (cdr (assoc :ordem-Servicos json-obj)))))))
		(setf *ordem-servico-id* id)))

(defun print-peca (peca i)
	(let* ((id (cdr (assoc :id peca)))
				 (nome (cdr (assoc :nome peca)))
				 (preco (cdr (assoc :preco peca))))
		(format t "~A:  peca: nome[~A] preco[~A] id[~A]~%" i nome preco id)))

(defun escolher-peca ()
	(let* ((body (do-get "estoque" nil))
				 (json-obj (cl-json:decode-json-from-string body)))
		(format t "~%~%~%~%~%~%~%~%~% ----- escolha a peca -------~%")
		(loop for peca in json-obj
					for i = 0 then (+ i 1)
					do (print-peca peca i))
		(format t "~%~%choose one: ")
		(let* ((user-input (parse-integer (read-line)))
					 (chosen-peca (nth user-input json-obj)))
			(setf *peca-id* (cdr (assoc :id chosen-peca)))
			(setf *peca-nome* (cdr (assoc :nome chosen-peca)))
			(setf *peca-preco* (cdr (assoc :preco chosen-peca))))))

(defun adicionar-peca (quantidade)
	(let* ((data (cl-json:encode-json-to-string `(("OrdemServicoId" . ,*ordem-servico-id*) ("Pecas" . ((("nome" . ,*peca-nome*) ("Quantidade" . ,quantidade) ("preco" . ,*peca-preco*) ("PecaId" . ,*peca-id*)))))))
				 (body (do-post "ordemservico/adicionaPeca/" data)))
		body))

(defun print-servico (servico i)
	(let* ((id (cdr (assoc :id servico)))
				 (nome (cdr (assoc :nome servico)))
				 (preco (cdr (assoc :preco servico))))
		(format t "~A:  servico: nome[~A] preco[~A] id[~A]~%" i nome preco id)))

(defun escolher-servico ()
	(let* ((body (do-get "Servico" nil))
				 (json-obj (cl-json:decode-json-from-string body)))
		(format t "~%~%~%~%~%~%~%~%~% ----- escolha o servico -------~%")
		(loop for servico in json-obj
					for i = 0 then (+ i 1)
					do (print-servico servico i))
		(format t "~%~%choose one: ")
		(let* ((user-input (parse-integer (read-line)))
					 (chosen-servico (nth user-input json-obj)))
			(setf *servico-id* (cdr (assoc :id chosen-servico)))
			(setf *servico-nome* (cdr (assoc :nome chosen-servico)))
			(setf *servico-preco* (cdr (assoc :preco chosen-servico))))))

(defun adiciona-servico ()
	(let* ((data (cl-json:encode-json-to-string `(("OrdemServicoId" . ,*ordem-servico-id*) ("Servicos" . ((("nome" . ,*servico-nome*) ("preco" . ,*servico-preco*) ("ServicoId" . ,*servico-id*)))))))
				 (body (do-post "ordemservico/adicionaServico/" data)))
		body))

(defun envia-orcamento ()
	(let* ((data (cl-json:encode-json-to-string `(("OrdemServicoId" . ,*ordem-servico-id*))))
				 (body (do-post "ordemservico/EnviarOrcamento/" data)))
		body))

(defun iniciar-diagnostico ()
	(let* ((data (cl-json:encode-json-to-string `(("ordemservicoid" . ,*ordem-servico-id*))))
				 (body (do-post "ordemservico/iniciardiagnostico/" data)))
		body))

(defun finalizar-diagnostico ()
	(let* ((data (cl-json:encode-json-to-string `(("ordemservicoid" . ,*ordem-servico-id*))))
				 (body (do-post "ordemservico/finalizarDiagnostico/" data)))
		body))

(defun iniciar-execucao ()
	(let* ((data (cl-json:encode-json-to-string `(("ordemservicoid" . ,*ordem-servico-id*))))
				 (body (do-post "ordemservico/iniciarExecucao/" data)))
		body))

(defun finalizar-execucao ()
	(let* ((data (cl-json:encode-json-to-string `(("ordemservicoid" . ,*ordem-servico-id*))))
				 (body (do-post "ordemservico/finalizarExecucao/" data)))
		body))

(defun entregar-veiculo ()
	(let* ((data (cl-json:encode-json-to-string `(("ordemservicoid" . ,*ordem-servico-id*))))
				 (body (do-post "ordemservico/entregarVeiculo/" data)))
		body))

(defun todas-metricas ()
	(let* ((body (do-get "metricas/" nil)))
		body))

(defun metrica-tempo-medio-atualizacao ()
	(let* ((url (concatenate 'string "metricas/ordemservico" *ordem-servico-id* "/tempo-medio/" ))
				 (body (do-get url nil)))
		body))

(defun metrica-tempo-total-atualizacao ()
	(let* ((url (concatenate 'string "metricas/ordemservico/" *ordem-servico-id* "/tempo-total/" ))
				 (body (do-get url nil)))
		body))

(defun media-todas-metricas ()
	(let* ((body (do-get "metricas/ordemservico/tempo-medio" nil)))
		body))





;;testes outras apis nao tao importantes
;;estoque
(defun get-peca-id ()
	(let* ((url (concatenate 'string "Estoque/" *peca-id*))
				 (body (do-get url nil)))
		body))

(defun create-peca (nome preco quantidade)
	(let* ((data (cl-json:encode-json-to-string `(("nome" . ,nome) ("preco" . ,preco) ("quantidade" . ,quantidade))))
				 (body (do-post "Estoque" data)))
		body))

(defun update-peca (nome preco quantidade)
	(let* ((url (concatenate 'string "Estoque/" *peca-id*))
				 (data (cl-json:encode-json-to-string `(("nome" . ,nome) ("preco" . ,preco) ("quantidade" . ,quantidade))))
				 (body (do-put url data)))
		body))

(defun do-atualiza-peca ()
	(escolher-peca)
	(let* ((nome (pede-input "Nome"))
				 (preco (pede-input "Preco"))
				 (quantidade (pede-input "Quantidade")))
		(update-peca nome preco quantidade)))

(defun do-deleta-peca ()
	(escolher-peca)
	(let* ((url (concatenate 'string "Estoque/" *peca-id*))
				 (body (do-delete url nil)))
		body))




;;servico
(defun do-cria-servico ()
	(let* ((nome (pede-input "Nome"))
				 (preco (pede-input "Preco"))
				 (data (cl-json:encode-json-to-string `(("nome" . ,nome) ("preco" . ,preco))))
				 (body (do-post "Servico" data)))
		body))

(defun do-atualiza-servico ()
	(escolher-servico)
	(let* ((nome (pede-input "Nome"))
				 (preco (pede-input "Preco")))
		(let* ((url (concatenate 'string "Servico/" *servico-id*))
				 (data (cl-json:encode-json-to-string `(("nome" . ,nome) ("preco" . ,preco))))
				 (body (do-put url data)))
		body)))

(defun do-deleta-servico ()
	(escolher-servico)
	(let* ((url (concatenate 'string "Servico/" *servico-id*))
				 (body (do-delete url nil)))
		body))




;;identidade
(defun get-usuario-by-email (email)
	(let* ((url (concatenate 'string "identidade/usuarios/" email))
				 (body (do-get url nil)))
		body))

(defun login-usuario ()
	(do-selecao-usuario)
	(do-login-cliente))




;;cliente
(defun get-cliente-by-id ()
	(let* ((url (concatenate 'string "cliente/" *cliente-id*))
				 (body (do-get url nil)))
		body))

(defun get-cliente-by-nome (nome)
	(let* ((url (concatenate 'string "cliente/bynome/" nome))
				 (body (do-get url nil)))
		body))

(defun do-atualiza-cliente ()
	(escolher-cliente)
	(let* ((nome (pede-input "Nome"))
				 (cpf (pede-input "CPF"))
				 (cnpj (pede-input "CNPJ"))
				 (tipopessoa (pede-input "Tipo Pessoa"))
				 (data (cl-json:encode-json-to-string `(("nome" . ,nome) ("cpf" . ,cpf) ("cnpj" . ,cnpj) ("tipopessoa" . ,tipopessoa))))
				 (url (concatenate 'string "cliente/" *cliente-id*))
				 (body (do-put url data)))
		body))

(defun get-clientes ()
	(let* ((body (do-get "cliente" nil)))
		body))

(defun print-cliente (cliente i)
	(let* ((id (cdr (assoc :id cliente)))
				 (nome (cdr (assoc :nome cliente)))
				 (cpf (cdr (assoc :cpf cliente)))
				 (cnpj (cdr (assoc :cnpj cliente)))
				 (tipopessoa (cdr (assoc :tipo-pessoa cliente))))
		(format t "~A:  cliente: nome[~A] cpf[~A] cnpj[~A] tipo[~A] id[~A]~%" i nome cpf cnpj tipopessoa id)))

(defun escolher-cliente ()
	(let* ((body (do-get "cliente" nil))
				 (json-obj (cl-json:decode-json-from-string body)))
		(format t "~%~%~%~%~%~%~%~%~% ----- escolha o cliente -------~%")
		(loop for cliente in json-obj
					for i = 0 then (+ i 1)
					do (print-cliente cliente i))
		(format t "~%~%choose one: ")
		(let* ((user-input (parse-integer (read-line)))
					 (chosen-cliente (nth user-input json-obj)))
			(setf *cliente-id* (cdr (assoc :id chosen-cliente)))
			(setf *cliente-nome* (cdr (assoc :nome chosen-cliente))))))

(defun do-deleta-cliente ()
	(escolher-cliente)
	(let* ((url (concatenate 'string "cliente/" *cliente-id*))
				 (body (do-delete url nil)))
		body))






;; veiculo
(defun do-cria-veiculo ()
	(let* ((placa (pede-input "Placa"))
				 (marca (pede-input "Marca"))
				 (modelo (pede-input "Modelo"))
				 (ano (parse-integer (pede-input "Ano"))))
		(cria-veiculo placa marca modelo ano)
		(seleciona-veiculo)))

(defun print-veiculo (veiculo i)
	(let* ((id (cdr (assoc :id veiculo)))
				 (placa (cdr (assoc :placa veiculo)))
				 (marca (cdr (assoc :marca veiculo)))
				 (modelo (cdr (assoc :modelo veiculo)))
				 (ano (cdr (assoc :ano veiculo))))
		(format t "[~A]  Veiculo: placa[~A] marca[~A] modelo[~A] ano[~A] id[~A]~%" i placa marca modelo ano id)))

(defun escolher-veiculo ()
	(let* ((body (do-get "veiculo" nil))
				 (json-obj (cl-json:decode-json-from-string body)))
		(format t "~%~%~%~%~%~%~%~%~% ----- escolha o veiculo -------~%")
		(loop for veiculo in json-obj
					for i = 0 then (+ i 1)
					do (print-veiculo veiculo i))
		(format t "~%~%choose one: ")
		(let* ((user-input (parse-integer (read-line)))
					 (chosen-veiculo (nth user-input json-obj)))
			(setf *veiculo-id* (cdr (assoc :id chosen-veiculo)))
			(setf *veiculo-placa* (cdr (assoc :placa chosen-veiculo))))))

(defun do-atualiza-veiculo ()
	(escolher-veiculo)
	(let* ((placa (pede-input "Placa"))
				 (marca (pede-input "Marca"))
				 (modelo (pede-input "Modelo"))
				 (ano (parse-integer (pede-input "Ano")))
				 (data (cl-json:encode-json-to-string `(("placa" . ,placa) ("marca" . ,marca) ("Modelo" . ,modelo) ("ano" . ,ano))))
				 (url (concatenate 'string "veiculo/" *veiculo-id*))
				 (body (do-put url data)))
		body))

(defun do-deleta-veiculo ()
	(escolher-veiculo)
	(let* ((url (concatenate 'string "veiculo/" *veiculo-id*))
				 (body (do-delete url nil)))
		body))






;;usuario 
(defun do-cria-usuario ()
	(let ((email (pede-input "Email"))
				(password (pede-input "password"))
				(roles (pede-varios-inputs "roles" '())))
		(cria-usuario password email roles)
		(setf *usuario-email* email)
		(format t "usuario ~a selecionado! ~%" *usuario-email*)
		(seleciona-usuario)))

(defun print-usuarios (usuarios)
	(format t "~%~%~%~%~%~%~%~% --------- Escolha o Usuario ------------~%")
	(loop for u in usuarios
				for i = 0 then (+ i 1)
				do (format t "[~a] email[~a] ~%" i
									 (cdr (assoc :email u)))))

(defun escolhe-usuario-da-lista (obj)
	(let* ((usuarios (cdr (assoc :data obj))))
		(print-usuarios usuarios)
		(let* ((user-input (parse-integer (read-line)))
					 (chosen-usuario (nth user-input usuarios)))
			(format t "chosen: ~a~%" chosen-usuario)
			(setf *usuario-id* (cdr (assoc :id chosen-usuario)))
			(setf *usuario-email* (cdr (assoc :email chosen-usuario))))))

(defun do-selecao-usuario ()
	(let* ((url (concatenate 'string "identidade/usuarios/"))
				 (body (do-get url nil))
				 (json-obj (cl-json:decode-json-from-string body)))
		(escolhe-usuario-da-lista json-obj)))

(defun do-cria-peca ()
	(let* ((nome (pede-input "Nome da Peça"))
				 (quantidade (pede-input "Quantidade"))
				 (preco (pede-input "Preco"))
				 (data (cl-json:encode-json-to-string `(("Nome" . ,nome) ("Quantidade" . ,quantidade) ("Preco" . ,preco))))
				 (body (do-post "estoque" data)))
		body))

(defun do-cria-cliente ()
	(let* ((nome (pede-input "Nome"))
				 (cpf (pede-input "CPF"))
				 (cnpj (pede-input "CNPJ"))
				 (tipoPessoa (pede-input "TipoPessoa")))
		(if (string-equal tipoPessoa "Fisica")
				(cria-cliente nome cpf "" tipopessoa)
				(cria-cliente nome "" cnpj tipopessoa))
		(setf *cliente-nome* nome)
		(seleciona-cliente)))





;; ===== OS ======
(defun do-cria-ordem-servico ()
	(cria-ordem-servico)
	(seleciona-ordem-servico))

(defun do-adiciona-peca ()
	(escolher-peca)
	(let* ((quantidade (pede-input "Quantidade")))
		(adicionar-peca quantidade)))

(defun do-adiciona-servico ()
	(escolher-servico)
	(adiciona-servico))

(defun print-os (os i)
	(let* ((id (cdr (assoc :id os)))
				 (total (cdr (assoc :total os)))
				 (data (cdr (assoc :data os)))
				 (status (cdr (assoc :status os)))
				 (clienteId (cdr (assoc :cliente-id os)))
				 (veiculoId (cdr (assoc :veiculo-id os))))
		(format t "[~A]  OS: id[~A] total[~A] status[~A] data[~A] id cliente[~A] id veiculo[~A]~%" i id total status data clienteId veiculoId)))

(defun escolher-ordem-servico ()
	(let* ((body (do-get "ordemServico" nil))
				 (json-obj (cl-json:decode-json-from-string body)))
		(format t "~%~%~%~%~%~%~%~%~% ----- escolha a ordem servico -------~%")
		(loop for os in json-obj
					for i = 0 then (+ i 1)
					do (print-os os i))
		(format t "~%~%choose one: ")
		(let* ((user-input (parse-integer (read-line)))
					 (chosen-os (nth user-input json-obj)))
			(setf *ordem-servico-id* (cdr (assoc :id chosen-os))))))

(defun aprovar-orcamento ()
	(let* ((body (do-get "tokens" nil))
				 (json-obj (cl-json:decode-json-from-string body)))
		(format t "~%~%~%~%~%~%~%~%~% ----- escolha a ordem servico -------~%")
		(loop for os in json-obj
					for i = 0 then (+ i 1)
					do (print-os os i))
		(format t "~%~%choose one: ")
		(let* ((user-input (parse-integer (read-line)))
					 (chosen-os (nth user-input json-obj)))
			(setf *ordem-servico-id* (cdr (assoc :id chosen-os))))))

(defun lista-ordem-servico ()
	(let* ((body (do-get "ordemServico" nil))
				 (json-obj (cl-json:decode-json-from-string body)))
		(format t "~%~%~%~%~%~%~%~%~% ----- lista de ordens servicos -------~%")
		(loop for os in json-obj
					for i = 0 then (+ i 1)
					do (print-os os i))))

(defparameter *identidade-comandos* '("login-admin" "seleciona-usuario" "cria-usuario" "login-usuario" "voltar"))
(defparameter *estoque-comandos* '("seleciona-peca" "cria-peca" "atualiza-peca" "deleta-peca" "voltar"))
(defparameter *servico-comandos* '("cria-servico" "seleciona-servico" "atualiza-servico" "deleta-servico" "voltar"))
(defparameter *cliente-comandos* '("cria-cliente" "seleciona-cliente" "atualiza-cliente" "deleta-cliente" "voltar"))
(defparameter *veiculo-comandos* '("cria-veiculo" "seleciona-veiculo" "atualiza-veiculo" "deleta-veiculo" "voltar"))
(defparameter *metrica-comandos* '("media-tempo" "tempo-medio-atualizacao" "tempo-total" "voltar"))
(defparameter *ordem-servico-comandos* '("cria-ordem-servico" "lista-ordem-servico" "seleciona-ordem-servico"  "adiciona-peca"  "adiciona-servico" "enviar-orcamento" "aprovar-orcamento" "rejeitar-orcamento" "iniciar-diagnostico" "finalizar-diagnostico" "iniciar-execucao" "finalizar-execucao" "entregar-veiculo" "deletar-ordem-servico" "voltar"))
(defparameter *comandos* '("identidade" "estoque" "servico" "cliente" "veiculo" "ordem servico" "metricas" "sair"))

(defun get-comando (comando)
	(cond ((string-equal comando "login-admin") #'do-login-admin)
				((string-equal comando "seleciona-usuario") #'do-selecao-usuario)
				((string-equal comando "login-usuario") #'login-usuario)
				((string-equal comando "cria-usuario") #'do-cria-usuario)
				((string-equal comando "seleciona-peca") #'escolher-peca)
				((string-equal comando "cria-peca") #'do-cria-peca)
				((string-equal comando "atualiza-peca") #'do-atualiza-peca)
				((string-equal comando "deleta-peca") #'do-deleta-peca)
				((string-equal comando "seleciona-servico") #'escolher-servico)
				((string-equal comando "cria-servico") #'do-cria-servico)
				((string-equal comando "atualiza-servico") #'do-atualiza-servico)
				((string-equal comando "deleta-servico") #'do-deleta-servico)
				((string-equal comando "cria-cliente") #'do-cria-cliente)
				((string-equal comando "seleciona-cliente") #'escolher-cliente)
				((string-equal comando "atualiza-cliente") #'do-atualiza-cliente)
				((string-equal comando "deleta-cliente") #'do-deleta-cliente)
				((string-equal comando "cria-veiculo") #'do-cria-veiculo)
				((string-equal comando "seleciona-veiculo") #'escolher-veiculo)
				((string-equal comando "atualiza-veiculo") #'do-atualiza-veiculo)
				((string-equal comando "deleta-veiculo") #'do-deleta-veiculo)
				((string-equal comando "cria-ordem-servico") #'do-cria-ordem-servico)
				((string-equal comando "lista-ordem-servico") #'lista-ordem-servico)
				((string-equal comando "seleciona-ordem-servico") #'escolher-ordem-servico)
				((string-equal comando "adiciona-peca") #'do-adiciona-peca)
				((string-equal comando "adiciona-servico") #'do-adiciona-servico)
				((string-equal comando "enviar-orcamento") #'envia-orcamento)
				((string-equal comando "iniciar-diagnostico") #'iniciar-diagnostico)
				((string-equal comando "finalizar-diagnostico") #'finalizar-diagnostico)
				((string-equal comando "iniciar-execucao") #'iniciar-execucao)
				((string-equal comando "finalizar-execucao") #'finalizar-execucao)
				((string-equal comando "entregar-veiculo") #'entregar-veiculo)
				((string-equal comando "media-tempo") #'media-todas-metricas)
				((string-equal comando "tempo-total") #'metrica-tempo-total-atualizacao)
				(t nil)))


(defun selecao-tipo-comando ()
	(format t "~%~%~%~%~%~%~%~%~% ----- escolha o tipo de comando -------~%")
		(loop for tipo in *comandos* 
					for i = 0 then (+ i 1)
					do (format t "[~a] ~a~%" i tipo))
		(format t "~%~%choose one: ")
		(let* ((user-input (parse-integer (read-line)))
					 (chosen-tipo (nth user-input *comandos*)))
			(setf *chosen-tipo* chosen-tipo)))

(defun get-comandos-lista (tipo)
	(cond ((string-equal tipo "identidade") *identidade-comandos*)
				((string-equal tipo "estoque") *estoque-comandos*)
				((string-equal tipo "servico") *servico-comandos*)
				((string-equal tipo "cliente") *cliente-comandos*)
				((string-equal tipo "veiculo") *veiculo-comandos*)
				((string-equal tipo "ordem servico") *ordem-servico-comandos*)
				((string-equal tipo "metricas") *metrica-comandos*)
				(t nil)))

(defun selecao-comando ()
	(format t "~%~%~%~%~%~%~%~%~% ----- escolha o comando -------~%")
	(let ((comandos-lista (get-comandos-lista *chosen-tipo*)))
		(loop for tipo in comandos-lista 
					for i = 0 then (+ i 1)
					do (format t "[~a] ~a~%" i tipo))
		(format t "~%~%choose one: ")
		(let* ((user-input (parse-integer (read-line)))
					 (chosen-comando (nth user-input comandos-lista)))
			(setf *chosen-comando* chosen-comando))))

(defun pede-input (nome-atributo)
	(format t "digite o ~A~%:" nome-atributo)
	(read-line))

(defun pede-varios-inputs (nome-atributo result)
	(format t "current ~a:~a~%addiciona mais um valor a ~A? [Y/n] ~%" nome-atributo result nome-atributo)
	(let ((resp (read-line)))
		(if (string-equal "n" resp)
				result
				(progn
					(format t "digite o valor:~%~%")
					(pede-varios-inputs nome-atributo (cons (read-line) result))))))

(defun main-loop ()
	(loop for
				tipo = (selecao-tipo-comando)
				until (string= tipo "sair")
				do (let* ((comando (selecao-comando)))
						 (when (not (string= comando "voltar"))
							 (funcall (get-comando comando))))))

;;;(main-loop)

(sb-ext:save-lisp-and-die "apicall" 
                         :toplevel #'main-loop 
                         :executable t)

