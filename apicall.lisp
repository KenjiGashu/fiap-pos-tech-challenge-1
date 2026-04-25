(ql:quickload :drakma)
(ql:quickload :cl-json)
(ql:quickload :shasht)

(setq *print-pretty* t)

(defparameter *jwt-token* "your.jwt.token.here")
(defparameter *admin-email* "Admin@gmail.com")
(defparameter *admin-password* "1234")
(defparameter *api-url* "http://localhost:5129/api/")
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

(defun do-post (api body)
	(let* ((url (concatenate 'string *api-url* api)))
		(format t "Doing POST request to ~A~%" url)
		(multiple-value-bind (body status-code headers uri stream must-close status-text)
				(drakma:http-request
				 url
				 :method :post
				 :content body
				 :additional-headers
				 `(("Content-Type" . "application/json")
					 ("Authorization" . ,(format nil "Bearer ~A" *jwt-token*))))

			(format t "Status: ~A ~A~%" status-code status-text)
			(when body
				(format t "Response Body: ~A~%" (shasht:write-json (shasht:read-json body) nil)))
			body)))

(defun do-get (api body)
	(let* ((url (concatenate 'string *api-url* api)))
		(format t "Doing GET request to ~A~%" url)
		(multiple-value-bind (body status-code headers uri stream must-close status-text)
				(drakma:http-request
				 url
				 :method :get
				 :content body
				 :additional-headers
				 `(("Content-Type" . "application/json")
					 ("Authorization" . ,(format nil "Bearer ~A" *jwt-token*))))

			(format t "Status: ~A ~A~%" status-code status-text)
			(when body
				(format t "Response Body: ~A~%" (shasht:write-json (shasht:read-json body) nil)))
			body)))

(defun do-login ()
	(let* ((data (cl-json:encode-json-to-string `(("password" . ,*admin-password*) ("email" . ,*admin-email*))))
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

(do-login)

(defun get-all-usuarios ()
	(let* ((body (do-get "identidade/usuarios" nil)))
		(format t "response: ~A~%" body)))

(get-all-usuarios)

(defun cria-usuario (password email roles)
	(let* ((data (cl-json:encode-json-to-string `(("password" . ,password) ("email" . ,email) ("roles" . ,roles))))
				 (body (do-post "identidade/usuarios/criar" data)))
		(format t "response: ~A~%" body)
		(setf *usuario-email* email)))

(cria-usuario "1234" "umemail@gmail.com" '("cliente" "teste"))

(defun seleciona-usuario ()
	(let* ((url (concatenate 'string "identidade/usuarios/" *usuario-email*))
				 (body (do-get url nil))
				 (json-obj (cl-json:decode-json-from-string body))
				 (email (cdr (assoc :email (cdr (assoc :usuarios json-obj)))))
				 (usuario-id (cdr (assoc :id (cdr (assoc :usuarios json-obj))))))
		(setf *usuario-email* email)
		(setf *usuario-id* usuario-id)))

(seleciona-usuario)

(defun cria-cliente (nome cpf cnpj tipopessoa)
	(let* ((data (cl-json:encode-json-to-string `(("Nome" . ,nome) ("UsuarioId" . ,*usuario-id*) ("Cpf" . ,cpf) ("Cnpj" . ,cnpj) ("TipoPessoa" . ,tipopessoa))))
				 (body (do-post "Cliente" data)))
		(setf *cliente-nome* nome)
		body))

(cria-cliente "apenasumnome" "433.023.538-20" "" "Fisica")

(defun seleciona-cliente ()
	(let* ((url (concatenate 'string "cliente/byNome/" *cliente-nome*))
				 (body (do-get url nil))
				 (json-obj (cl-json:decode-json-from-string body))
				 (id (cdr (assoc :id json-obj))))
		(setf *cliente-id* id)))

(seleciona-cliente)

(defun cria-veiculo (placa marca modelo ano)
	(let* ((data (cl-json:encode-json-to-string `(("placa" . ,placa) ("marca" . ,marca) ("modelo" . ,modelo) ("ano" . ,ano))))
				 (body (do-post "veiculo" data)))
		(setf *veiculo-placa* placa)
		body))

(cria-veiculo "TKJ5A20" "toyota" "yaris" 2000)

(defun seleciona-veiculo ()
	(let* ((url (concatenate 'string "veiculo/byPlaca/" *veiculo-placa*))
				 (body (do-get url nil))
				 (json-obj (cl-json:decode-json-from-string body))
				 (id (cdr (assoc :id json-obj))))
		(setf *veiculo-id* id)))

(seleciona-veiculo)

(defun cria-ordem-servico ()
	(let* ((data (cl-json:encode-json-to-string `(("ClienteId" . ,*cliente-id*) ("VeiculoId" . ,*veiculo-id*))))
				 (body (do-post "ordemservico" data)))
		body))

(cria-ordem-servico)

(defun all-ordem-servico ()
	(let* ((body (do-get "ordemServico" nil))
				 (json-obj (cl-json:decode-json-from-string body))
				 (id (cdr (assoc :id json-obj))))
		body))

(all-ordem-servico)

(defun seleciona-ordem-servico ()
	(let* ((url (concatenate 'string "ordemservico/cliente/" *cliente-id*))
				 (body (do-get url nil))
				 (json-obj (cl-json:decode-json-from-string body))
				 (id (cdr (assoc :id (first (cdr (assoc :ordem-Servicos json-obj)))))))
		(setf *ordem-servico-id* id)))

(seleciona-ordem-servico)

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

(escolher-peca)

(defun adicionar-peca ()
	(let* ((data (cl-json:encode-json-to-string `(("OrdemServicoId" . ,*ordem-servico-id*) ("Pecas" . ((("nome" . ,*peca-nome*) ("Quantidade" . 1) ("preco" . ,*peca-preco*) ("PecaId" . ,*peca-id*)))))))
				 (body (do-post "ordemservico/adicionaPeca/" data)))
		body))

(adicionar-peca)

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

(escolher-servico)

(defun adiciona-servico ()
	(let* ((data (cl-json:encode-json-to-string `(("OrdemServicoId" . ,*ordem-servico-id*) ("Servicos" . ((("nome" . ,*servico-nome*) ("preco" . ,*servico-preco*) ("ServicoId" . ,*servico-id*)))))))
				 (body (do-post "ordemservico/adicionaServico/" data)))
		body))

(adiciona-servico)

(defun envia-orcamento ()
	(let* ((data (cl-json:encode-json-to-string `(("OrdemServicoId" . ,*ordem-servico-id*))))
				 (body (do-post "ordemservico/EnviarOrcamento/" data)))
		body))

(envia-orcamento)

(defun iniciar-diagnostico ()
	(let* ((data (cl-json:encode-json-to-string `(("ordemservicoid" . ,*ordem-servico-id*))))
				 (body (do-post "ordemservico/iniciardiagnostico/" data)))
		body))

(iniciar-diagnostico)

(defun finalizar-diagnostico ()
	(let* ((data (cl-json:encode-json-to-string `(("ordemservicoid" . ,*ordem-servico-id*))))
				 (body (do-post "ordemservico/finalizarDiagnostico/" data)))
		body))

(finalizar-diagnostico)

(defun iniciar-execucao ()
	(let* ((data (cl-json:encode-json-to-string `(("ordemservicoid" . ,*ordem-servico-id*))))
				 (body (do-post "ordemservico/iniciarExecucao/" data)))
		body))

(iniciar-execucao)

(defun finalizar-execucao ()
	(let* ((data (cl-json:encode-json-to-string `(("ordemservicoid" . ,*ordem-servico-id*))))
				 (body (do-post "ordemservico/finalizarExecucao/" data)))
		body))

(finalizar-execucao)

(defun entregar-veiculo ()
	(let* ((data (cl-json:encode-json-to-string `(("ordemservicoid" . ,*ordem-servico-id*))))
				 (body (do-post "ordemservico/entregarVeiculo/" data)))
		body))

(entregar-veiculo)

(do-get "cliente" (cl-json:encode-json-to-string `(("password" . ,*admin-password*) ("email" . ,*admin-email*))))
(do-get "identidade/usuarios" (cl-json:encode-json-to-string `(("password" . ,*admin-password*) ("email" . ,*admin-email*))))

(do-login "identidade/login" (cl-json:encode-json-to-string `(("password" . ,*admin-password*) ("email" . ,*admin-email*))))

(do-post "identidade/usuarios/criar" (cl-json:encode-json-to-string `(("password" . "oioi") ("email" . "oi@gmail.com") ("roles" . ("cliente")))))

(do-post "cliente" (cl-json:encode-json-to-string `(("Nome" . "umNome") ("UsuarioId" . "DE1E8FFF-6184-4DB8-8641-4CB00B71B85E") ("Cpf" . "433.023.538-20") ("TipoPessoa" . "Fisica") ("Cnpj" . ""))))

;; authenticated
(let* ((data `(("password" . *admin-password*)
               ("email" . *admin-email*)))
       (json-body (cl-json:encode-json-to-string data)))
  (let* ((response (drakma:http-request
										"http://localhost:5129/api/cliente"
										:method :get
										:content json-body
										:additional-headers
										`(("Content-Type" . "application/json")
											("Authorization" . ,(format nil "Bearer ~A" *jwt-token*)))))
				 (json-obj (cl-json:decode-json-from-string response))
				 (test (shasht:read-json response))
				 (resp (get-token json-obj)))
		
		(format t "aodkqwoeqwe: ~A~%" (shasht:write-json test nil))
		
		
		(setf *jwt-token* resp)
		(format t "Resposta bruta: ~A~%" response)
		
		(let ((json (cl-json:decode-json-from-string response)))
			(format t "JSON parseado: ~A~%" json))))

;; post
(let* ((data `(("password" . ,*admin-password*)
               ("email" . ,*admin-email*)))
       (json-body (cl-json:encode-json-to-string data)))
  (let* ((response (drakma:http-request
										"http://localhost:5129/api/identidade/login"
										:method :post
										:content json-body
										:additional-headers
										`(("Content-Type" . "application/json"))))
				 (json-obj (cl-json:decode-json-from-string response))
				 (test (shasht:read-json response))
				 (resp (get-token json-obj)))
		
		(format t "aodkqwoeqwe: ~A~%" (shasht:write-json test nil))
		(setf *jwt-token* resp)
		(format t "Resposta bruta: ~A~%" response)
		
		(let ((json (cl-json:decode-json-from-string response)))
			(format t "JSON parseado: ~A~%" json))))


(multiple-value-bind (body status-code headers uri stream must-close status-text)
    (drakma:http-request "https://github.com")
  (format t "Status: ~A ~A~%" status-code status-text)
  (format t "Response Body: ~A~%" body))

(multiple-value-bind (body status-code headers uri stream must-close status-text)
    (drakma:http-request "http://localhost:5129/api/identidade/usuarios"
												 :method :get)
  (format t "Status: ~A ~A~%" status-code status-text)
  (format t "Response Body: ~A~%" body)
	;; (let ((data (json:decode-json-from-string bodyrepo => repo.Criar(It.Is<OrdemServico>(os => os.Status == StatusOrdemServico.Recebida)))))
	;; 	(assoc :usuarios body))
	(assoc :email (cadr (assoc :usuarios (cl-json:decode-json-from-string body))))
	)
