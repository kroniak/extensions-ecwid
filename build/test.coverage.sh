#!/bin/bash
set -euo pipefail

command -v dotnet >/dev/null 2>&1 || {
    echo >&2 "This script requires the dotnet core sdk tooling to be installed"
    exit 1
}

SCRIPT_ROOT="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

echo "!!WARNING!! This script runs tests and checks code coverage for netstandard and netcoreapp targets"

dotnet test -c Release /p:CollectCoverage=true \
/p:Exclude="[xunit.*]*" \
/p:CoverletOutputFormat=lcov /p:CoverletOutput=../../lcov \
"${SCRIPT_ROOT}/../test/Ecwid.Test/"