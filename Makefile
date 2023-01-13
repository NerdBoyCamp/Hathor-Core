#change this to the depth of the project folders
#if needed, add a prefix for a common project folder
CSHARP_SOURCE_FILES = $(wildcard */*/*.cs */*.cs *.cs)

#add needed flags to the compilerCSHARP_FLAGS = -out:$(BUILD_EXEC)
CSHARP_FLAGS = -out:$(BUILD_EXEC) -target:exe -r:$(BUILD_DEPS)

#change to the environment compiler
CSHARP_COMPILER = csc

#if needed, change the executable file
BUILD_PATH = Builds
BUILD_EXEC = ${BUILD_PATH}/HathorCore.exe
BUILD_DEPS = "C:\Program Files\WindowsPowerShell\Modules\newtonsoft.json\1.0.2.201\libs\Newtonsoft.Json.dll"

ifeq ($(OS), Windows_NT)     # is Windows_NT on XP, 2000, 7, Vista, 10...
  detected_OS := Windows
else
  detected_OS := $(shell uname)  # same as "uname -s"
endif

ifeq ($(detected_OS),Windows)
  CMD_CREATE_PATH = md
  CMD_DELETE_PATH = rmdir /s /q
else
  CMD_CREATE_PATH = mkdir -p
  CMD_DELETE_PATH = rm -fr
endif

build:
	${CMD_CREATE_PATH} ${BUILD_PATH}
	$(CSHARP_COMPILER) $(CSHARP_SOURCE_FILES) $(CSHARP_FLAGS)

clean:
	$(CMD_DELETE_PATH) $(BUILD_PATH)

remake:
	$(MAKE) clean
	$(MAKE)
