resource "kubernetes_persistent_volume_claim" "postgres" {

  metadata {
    name      = "postgres-pvc"
    namespace = kubernetes_namespace.gashu.metadata[0].name
  }

  spec {

    access_modes = [
      "ReadWriteOnce"
    ]

    resources {

      requests = {
        storage = "5Gi"
      }

    }

  }

}

resource "kubernetes_deployment" "postgres" {

  metadata {

    name      = "postgres"
    namespace = kubernetes_namespace.gashu.metadata[0].name

    labels = {
      app = "postgres"
    }

  }

  spec {

    replicas = 1

    selector {

      match_labels = {
        app = "postgres"
      }

    }

    template {

      metadata {

        labels = {
          app = "postgres"
        }

      }

      spec {

        container {

          name  = "postgres"
          image = "postgres:16-alpine"

          port {
            container_port = 5432
          }

          env {

            name  = "POSTGRES_DB"
            value = var.postgres_database

          }

          env {

            name  = "POSTGRES_USER"
            value = var.postgres_user

          }

          env {

            name = "POSTGRES_PASSWORD"

            value_from {

              secret_key_ref {

                name = kubernetes_secret.sistema_mecanica.metadata[0].name
                key  = "POSTGRES_PASSWORD"

              }

            }

          }

          volume_mount {

            name       = "postgres-storage"
            mount_path = "/var/lib/postgresql/data"

          }

        }

        volume {

          name = "postgres-storage"

          persistent_volume_claim {

            claim_name = kubernetes_persistent_volume_claim.postgres.metadata[0].name

          }

        }

      }

    }

  }

}

resource "kubernetes_service" "postgres" {

  metadata {

    name      = "db"
    namespace = kubernetes_namespace.gashu.metadata[0].name

  }

  spec {

    selector = {
      app = "postgres"
    }

    port {

      port        = 5432
      target_port = 5432

    }

    type = "ClusterIP"

  }

}