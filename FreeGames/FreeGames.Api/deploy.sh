#!/bin/bash
gcloud functions deploy FreeGames.Api \
    --entry-point FreeGames.Api.CloudFunction \
    --runtime dotnet3 \
    --trigger-http \
    --max-instances 1