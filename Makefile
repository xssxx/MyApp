SOLUTION_NAME=MyApp
SRC_DIR=src
TESTS_DIR=tests

# Path shortcuts
API=$(SRC_DIR)/Api
INFRA=$(SRC_DIR)/Infrastructure
DOMAIN=$(SRC_DIR)/Domain
LIBRARY=$(SRC_DIR)/Library
APP=$(SRC_DIR)/Application
API_TESTS=$(TESTS_DIR)/Api.Tests

# Targets

init:
	@echo "📁 Creating folders and projects..."
	mkdir -p $(SRC_DIR) $(TESTS_DIR)
	dotnet new sln -n $(SOLUTION_NAME)
	dotnet new webapi -o $(API)
	dotnet new classlib -o $(INFRA)
	dotnet new classlib -o $(DOMAIN)
	dotnet new classlib -o $(LIBRARY)
	dotnet new classlib -o $(APP)
	dotnet new xunit -o $(API_TESTS)

	@echo "➕ Adding projects to solution..."
	dotnet sln add $(API) $(INFRA) $(DOMAIN) $(LIBRARY) $(APP) $(API_TESTS)

	@echo "🔗 Adding project references..."
	dotnet add $(API) reference $(APP)
	dotnet add $(APP) reference $(LIBRARY) $(DOMAIN)
	dotnet add $(INFRA) reference $(DOMAIN)
	dotnet add $(LIBRARY) reference $(INFRA) $(DOMAIN)
	dotnet add $(API_TESTS) reference $(API)

migrate:
	@echo "🧱 Running EF migrations..."
	dotnet ef migrations add InitialCreate -p $(INFRA) -s $(API)

update-db:
	@echo "📦 Updating database..."
	dotnet ef database update -p $(INFRA) -s $(API)

build:
	@echo "🏗️  Building solution..."
	dotnet build

run:
	@echo "🚀 Running API..."
	dotnet run --project $(API)

test:
	@echo "🧪 Running tests..."
	dotnet test

clean:
	@echo "🧹 Cleaning up build artifacts..."
	dotnet clean

