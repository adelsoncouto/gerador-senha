#!/bin/bash

dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o ./publish/windows \
    && dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -o ./publish/linux
