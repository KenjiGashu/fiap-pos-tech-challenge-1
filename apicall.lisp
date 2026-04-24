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

(defun do-login (api body)
	(let* ((url (concatenate 'string *api-url* api))
				 (response (drakma:http-request
										url
										:method :post
										:content body
										:additional-headers
										`(("Content-Type" . "application/json")))))
		(format t "response: ~A~%" response)
		
		(let* ((json-obj (cl-json:decode-json-from-string response))
					 (resp (get-token json-obj)))
			(setf *jwt-token* resp))))

(defun seleciona-usuario (email)
	(let* ((url (concatenate 'string "identidade/usuarios/" email))
				 (body (do-get url nil))
				 (json-obj (cl-json:decode-json-from-string body))
				 (email (cdr (assoc :email (cdr (assoc :usuarios json-obj)))))
				 (usuario-id (cdr (assoc :id (cdr (assoc :usuarios json-obj))))))
		(setf *usuario-email* email)
		(setf *usuario-id* usuario-id)))

(seleciona-usuario "oi@gmail.com")


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
