# Project vars
PROJECT_DIR := Projeto/
PROJECT_TITLE := DiagnosticoDeSaude


# Directories vars
RELEASE_DIR := Projeto/bin/Release/
DEBUG_DIR := Projeto/bin/Debug/

RELEASES_DIR := releases/

# NuGet vars
NUGET_PACKAGES_DIR := packages/

# Tools vars
MDTOOL := mdtool

# Version vars
BIN_NAME := Projeto.exe
VERSION  := 1.0.0
CODENAME := baby


# Compress vars
TARGET_ZIP := $(PROJECT_TITLE)-$(VERSION)-$(CODENAME).zip

.PHONY : build-release build-debug zip-release

build-release: $(PROJECT_DIR)
	$(MDTOOL) build -t:Build -c:Release

build-debug: $(PROJECT_DIR)
	$(MDTOOL) build -t:Build -c:Debug

release: build-release
	mono $(RELEASE_DIR)/$(BIN_NAME)

debug: build-debug
	mono $(DEBUG_DIR)/$(BIN_NAME)

zip-release: build-release
	zip -r $(RELEASES_DIR)/$(TARGET_ZIP) $(RELEASE_DIR)