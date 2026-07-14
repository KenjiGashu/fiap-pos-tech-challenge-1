resource "kubernetes_deployment" "sistema_mecanica" {
  metadata {
    name      = "sistema-mecanica"
    namespace = kubernetes_namespace.gashu.metadata[0].name
    labels = {
      app = "sistema-mecanica"
    }
  }

  spec {
    replicas = 1
    selector {
      match_labels = {
        app = "sistema-mecanica"
      }
    }

    template {
      metadata {
        labels = {
          app = "sistema-mecanica"
        }
      }

      spec {
        container {
          name  = var.app_name
          image = "${var.api_image}:${var.image_tag}"

          image_pull_policy = "IfNotPresent"

          port {
            container_port = 8080
          }

          resources {
            requests = {
              cpu    = "200m"
              memory = "256Mi"
            }

            limits = {
              cpu    = "500m"
              memory = "512Mi"
            }
          }

          env_from {
            config_map_ref {
              name = kubernetes_config_map.sistema_mecanica.metadata[0].name
            }
          }

          env_from {
            secret_ref {
              name = kubernetes_secret.sistema_mecanica.metadata[0].name
            }
          }

          liveness_probe {
            http_get {
              path = "/health"
              port = 8080
            }

            initial_delay_seconds = 20
            period_seconds        = 15
          }

          readiness_probe {
            http_get {
              path = "/health"
              port = 8080
            }

            initial_delay_seconds = 10
            period_seconds        = 10
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "api" {
  metadata {
    name      = "sistema-mecanica-service"
    namespace = kubernetes_namespace.gashu.metadata[0].name
  }

  spec {
    selector = {
      app = "sistema-mecanica"
    }

    port {
      port        = 80
      target_port = 8080
    }

    type = "ClusterIP"
  }
}

resource "kubernetes_service" "api_externo" {
  metadata {
    name      = "sistema-mecanica-externo"
    namespace = kubernetes_namespace.gashu.metadata[0].name
  }

  spec {
    selector = {
      app = "sistema-mecanica"
    }

    port {
      port        = 80
      target_port = 8080
      node_port   = 30080
    }

    type = "NodePort"
  }
}