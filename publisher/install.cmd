@echo off
SET RARLINE="%ProgramFiles%\WinRAR\Winrar.exe"
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./storages
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/configdata
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/wwwroot/js
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/wwwroot/css
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/wwwroot/images
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/wwwroot/lib/*.min.js
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/wwwroot/lib/*.min.css
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/wwwroot/lib/font-awesome/fonts
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/*.dll
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/Yd.*.json
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./website/Yd.*.xml
%RARLINE% a -m1 -r -o+ -s -x%0 ./install.rar ./config