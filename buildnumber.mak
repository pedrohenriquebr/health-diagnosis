$(BUILD_NUMBER_FILE): $(OBJ_DIR)/Release/$(BIN_NAME) 
	if ! test -f $(BUILD_NUMBER_FILE); then \
		echo 0 > $(BUILD_NUMBER_FILE); \
	fi
	echo $$(($$(cat $(BUILD_NUMBER_FILE)) + 1)) > $(BUILD_NUMBER_FILE)	
