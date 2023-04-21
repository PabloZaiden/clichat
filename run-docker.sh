#!/usr/bin/env bash

set -e

source ./docker/image.env

docker run \
    --rm -ti \
    -e OPENAI_API_KEY=$OPENAI_API_KEY \
    -v $(pwd)/api.key:/app/api.key \
    $dockerImage:$dockerTag