resource "kubernetes_secret" "sistema_mecanica" {
  metadata {
    name      = "sistema-mecanica-secret"
    namespace = kubernetes_namespace.gashu.metadata[0].name
  }
	
  data = {
    POSTGRES_PASSWORD = var.postgres_password
    FIAP_POS_SECRET = var.jwt_secret
    FIAP_POS_SALT = var.password_salt
    FIAP_POS_APP_PASSWORD = var.email_password
    ConnectionStrings__Default = "Host=db;Port=${var.postgres_port};Database=${var.postgres_database};Username=${var.postgres_user};Password=${var.postgres_password}"
  }

  type = "Opaque"
}