#!/bin/sh

chmod a+x ./NServer
LD_LIBRARY_PATH=${LD_LIBRARY_PATH}:../../../Lib/Linux_armhf/ ./NServer -c NServer.SQLite_Sample.conf  "$@"
