variable "namespace" {
  description = "Namespace da aplicação"
  type        = string
  default     = "gashu-app"
}

variable "app_name" {
  description = "Nome da aplicação"
  type        = string
  default     = "sistema-mecanica"
}

variable "api_image" {
  default = "sistema-mecanica:1.0.0"
}

variable "aspnet_environment" {
  type    = string
  default = "Production"
}

variable "app_port" {
  type    = number
  default = 8080
}

variable "email" {
  type    = string
  default = "kenjigashu@gmail.com"
}

variable "postgres_password" {
  description = "Senha do PostgreSQL"
  type        = string
  sensitive   = true
}

variable "jwt_secret" {
  description = "JWT Secret"
  type        = string
  sensitive   = true
}

variable "password_salt" {
  description = "Salt utilizado pela aplicação"
  type        = string
  sensitive   = true
}

variable "email_password" {
  description = "Senha do e-mail"
  type        = string
  sensitive   = true
}

variable "postgres_host" {
  default = "db"
}

variable "postgres_database" {
  default = "mydb"
}

variable "postgres_user" {
  default = "myuser"
}

variable "postgres_port" {
  default = 5432
}