@echo off
SET RARLINE="%ProgramFiles%\WinRAR\Winrar.exe"
%RARLINE% a -m1 -r -o+ -s -x%0 ./update.rar ./website/configdata
%RARLINE% a -m1 -r -o+ -s -x%0 ./update.rar ./website/wwwroot/js
%RARLINE% a -m1 -r -o+ -s -x%0 ./update.rar ./website/wwwroot/css
%RARLINE% a -m1 -r -o+ -s -x%0 ./update.rar ./website/wwwroot/images
%RARLINE% a -m1 -r -o+ -s -x%0 ./update.rar ./website/wwwroot/lib/*.min.js
%RARLINE% a -m1 -r -o+ -s -x%0 ./update.rar ./website/wwwroot/lib/*.min.css
%RARLINE% a -m1 -r -o+ -s -x%0 ./update.rar ./website/wwwroot/lib/font-awesome/fonts
%RARLINE% a -m1 -r -o+ -s -x%0 ./update.rar ./website/*.dll
%RARLINE% a -m1 -r -o+ -s -x%0 ./update.rar ./website/Yd.*.json