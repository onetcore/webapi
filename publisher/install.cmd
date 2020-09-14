@echo off
SET RARLINE="%ProgramFiles%\WinRAR\Winrar.exe"
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./storages
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./config
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/configdata
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/runtimes
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/zh-Hans
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/wwwroot/favicon.ico
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/wwwroot/js/*.min.js
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/wwwroot/css/*.min.css
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/wwwroot/images
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/wwwroot/files
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/wwwroot/lib/*.min.js
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/wwwroot/lib/*.min.css
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/wwwroot/lib/font-awesome/fonts
%RARLINE% a -m1 -o+ -s -x%0 ./install.rar ./website/*.dll
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/Yd.deps.json
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/Yd.runtimeconfig.json
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/Yd.*.xml
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/Yd.exe