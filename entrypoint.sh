#!/bin/sh
export PORT="${PORT:-5000}"
export ASPNETCORE_URLS="http://+:${PORT}"

"$@"
