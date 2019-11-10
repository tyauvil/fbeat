#!/usr/bin/env bash
export PORT="${PORT:-5000}"
export ASPNETCORE_URLS="http://+:${PORT}"

exec "$@"
