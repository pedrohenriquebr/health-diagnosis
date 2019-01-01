# Project vars
PROJECT_DIR := Projeto/
PROJECT_TITLE := DiagnosticoDeSaude
PROJECT_SOLUTION := DiagnosticoSaude.sln

# Directories vars
BIN_DIR := $(PROJECT_DIR)/bin/
OBJ_DIR := $(PROJECT_DIR)/obj/

RELEASE_DIR := $(BIN_DIR)/Release/
DEBUG_DIR := $(BIN_DIR)/Debug/

RELEASES_DIR := $(PWD)/releases/

# NuGet vars
NUGET_PACKAGES_DIR := $(PWD)/packages/

# Tools vars
MSBUILD := msbuild

# Version vars
BIN_NAME := Projeto.exe
BUILD_NUMBER_FILE := $(PWD)/build-number.txt
VERSION  = 1.0.$$(cat $(BUILD_NUMBER_FILE))
CODENAME := child

# Compress vars
TARGET_ZIP := $(PROJECT_TITLE)-$(VERSION).zip


# DotEnvironment vars
DOTENV := .env

BUILD_DIR := $(PWD)/build

include buildnumber.mak

.PHONY : build-release build-debug zip-release clean debug rleease 

restore-packages: DiagnosticoSaude.sln
	nuget restore .

define build
	$(MSBUILD) $(PROJECT_SOLUTION) /t:Build /p:Configuration=${1}
endef

build-release: $(BUILD_NUMBER_FILE) $(PROJECT_DIR) restore-packages
	$(call build,Release)

build-debug: $(PROJECT_DIR) restore-packages
	$(call build,Debug)

release: $(RELEASE-DIR)
	mono $(RELEASE_DIR)/$(BIN_NAME)

debug: $(DEBUG_DIR)
	mono $(DEBUG_DIR)/$(BIN_NAME)

zip-release: build-release
	mkdir -p $(BUILD_DIR); \
	mkdir -p $(RELEASES_DIR); \
	cp $(RELEASE_DIR)/*.* $(BUILD_DIR) ; \
	cd $(BUILD_DIR) ; \
	zip  $(TARGET_ZIP) *.* ;\
	mv $(TARGET_ZIP) $(RELEASES_DIR)
	echo $(VERSION)
clean:
	rm -rf $(BUILD_DIR)
	$(MSBUILD) /t:Clean
