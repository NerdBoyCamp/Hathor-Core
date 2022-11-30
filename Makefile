#change this to the depth of the project folders
#if needed, add a prefix for a common project folder
CSHARP_SOURCE_FILES = $(wildcard */*/*.cs */*.cs *.cs)

#add needed flags to the compilerCSHARP_FLAGS = -out:$(BUILD_EXEC)
CSHARP_FLAGS = -out:$(BUILD_EXEC) -target:exe

#change to the environment compiler
CSHARP_COMPILER = mcs

#if needed, change the executable file
BUILD_PATH = Dist
BUILD_EXEC = ${BUILD_PATH}/HathorCore


all: build

build: 
	mkdir -p ${BUILD_PATH}
	$(CSHARP_COMPILER) $(CSHARP_SOURCE_FILES) $(CSHARP_FLAGS)

clean:
	rm -f $(BUILD_EXEC)

remake:
	$(MAKE) clean
	$(MAKE)

