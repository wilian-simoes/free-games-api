#!/bin/bash
gcloud functions deploy FreeGamesAPI \
    --entry-point FreeGamesAPI.CloudFunction \
    --runtime dotnet3 \
    --trigger-http \
    --max-instances 1