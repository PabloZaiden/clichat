#!/usr/bin/env bash

set -e

source ./docker/image.env

# Build the docker image
docker build -t $dockerImage:$dockerTag -f ./docker/Dockerfile .