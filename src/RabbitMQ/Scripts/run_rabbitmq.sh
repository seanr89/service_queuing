#!/bin/sh

set -e -u #Exit immediately if a command exits with a non-zero status.

echo "Executing script!"

docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.11-management

echo "App Complete!"