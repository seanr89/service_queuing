#!/bin/sh

set -e -u #Exit immediately if a command exits with a non-zero status.

echo "Executing Docker Script!"

cd ../
echo "Building Docker Image - Background Sender"
docker build . -f Dockerfile.BackgroundSender -t 'backgroundsender:latest'

echo "Building Docker Image - Automated Sender"
docker build . -f Dockerfile.AutomatedSender -t 'automatedsender:latest'

echo "Building Docker Image - Receiver Host"
docker build . -f Dockerfile.ReceiverHost -t 'receiverhost:latest'

echo "Building Docker Image - Sender"
docker build . -f Dockerfile.Sender -t 'sender:latest'

echo "App Complete!"