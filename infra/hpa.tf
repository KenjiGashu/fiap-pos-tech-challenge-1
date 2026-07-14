resource "kubernetes_horizontal_pod_autoscaler_v2" "api" {
  metadata {
    name      = "sistema-mecanica-hpa"
    namespace = kubernetes_namespace.gashu.metadata[0].name
  }

  spec {
    min_replicas = 1
    max_replicas = 3

    scale_target_ref {
      api_version = "apps/v1"
      kind        = "Deployment"
      name        = kubernetes_deployment.sistema_mecanica.metadata[0].name
    }

    metric {
      type = "Resource"
      resource {
        name = "cpu"
        target {
          type                = "Utilization"
          average_utilization = 50
        }
      }
    }
  }
}