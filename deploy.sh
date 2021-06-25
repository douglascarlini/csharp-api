#!/bin/bash

NAME=$1

{ docker build -t $NAME .; } || { exit; }
{ docker stop $NAME &>/dev/null; } || { echo "stop OK"; }
{ docker rm $NAME &>/dev/null; } || { echo "delete OK"; }
{ docker network create $NAME &>/dev/null; } || { echo "network OK"; }
{ docker run --rm --name $NAME --network $NAME -d $NAME; } || { echo "Deploy error"; }
