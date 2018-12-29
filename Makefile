# Project vars
PROJECT_DIR := Projeto/
PROJECT_TITLE := DiagnosticoDeSaude


# Directories vars
BIN_DIR := $(PROJECT_DIR)/bin/
OBJ_DIR := $(PROJECT_DIR)/obj/

RELEASE_DIR := $(BIN_DIR)/Release/
DEBUG_DIR := $(BIN_DIR)/Debug/

RELEASES_DIR := $(PWD)/releases/

# NuGet vars
NUGET_PACKAGES_DIR := $(PWD)/packages/

# Tools vars
MDTOOL := mdtool

# Version vars
BIN_NAME := Projeto.exe
VERSION  := 1.0.0
CODENAME := baby


# Compress vars
TARGET_ZIP := $(PROJECT_TITLE)-$(VERSION).zip

BUILD_DIR := $(PWD)/build

.PHONY : build-release build-debug zip-release clean

restore-packages: DiagnosticoSaude.sln
	nuget restore .

build-release: $(PROJECT_DIR) restore-packages
	$(MDTOOL) build -t:Build -c:Release

build-debug: $(PROJECT_DIR) restore-packages
	$(MDTOOL) build -t:Build -c:Debug

release: build-release
	mono $(RELEASE_DIR)/$(BIN_NAME)

debug: build-debug
	mono $(DEBUG_DIR)/$(BIN_NAME)

zip-release: build-release
	mkdir -p $(BUILD_DIR); \
	mkdir -p $(RELEASES_DIR); \
	cp $(RELEASE_DIR)/*.* $(BUILD_DIR) ; \
	cd $(BUILD_DIR) ; \
	zip  $(TARGET_ZIP) *.* ;\
	mv $(TARGET_ZIP) $(RELEASES_DIR)

clean:
	rm -rf $(BUILD_DIR)
	rm -rf $(BIN_DIR)
	rm -rf $(OBJ_DIR)
	rm -rf $(NUGET_PACKAGES_DIR)
