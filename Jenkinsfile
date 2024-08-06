pipeline {
     agent { label 'windows' }
  
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                script {
                    // Restoring dependencies
                    bat "dotnet restore"

                    // Building the application
                    bat "dotnet build --configuration Debug" 
                }
            }
        }

        stage('Test') {
            steps {
                script {
                    // Running tests
                    bat "dotnet test --no-restore --configuration Debug"
                }
            }
        }

        stage('Publish') {
            steps {
                script {
                    // Publishing the application
                    bat "dotnet publish --no-restore --configuration Debug --output .\\publish"
                }
            }
        }
        
        stage('Deploy') {
            steps {
                script {
                    echo 'Deploy project'
                }
            }
        }
    }

    post {
        success {
            echo 'Build, test, and publish successful!'
        }
    }
}
