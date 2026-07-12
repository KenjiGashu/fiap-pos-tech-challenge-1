resource "kubernetes_config_map" "sistema_mecanica" {
  metadata {
    name      = "sistema-mecanica-config"
    namespace = kubernetes_namespace.gashu.metadata[0].name
  }

	data = {
    ASPNETCORE_URLS        = "http://+:${var.app_port}"
    ASPNETCORE_ENVIRONMENT = var.aspnet_environment

    FIAP_POS_PORT  = tostring(var.app_port)
    FIAP_POS_EMAIL = var.email
  }
}