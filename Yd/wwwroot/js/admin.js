///<reference path="/wwwroot/lib/jquery/jquery.js"/>
var tester = {
    submit: function (current) {
        current = $(current).parents('.modal').find('form');
        var result = current.find('.test-result').html('');
        var method = current.attr('method');
        var url = current.attr('action') || location.href;
        var headers = {};
        if (window.$token) headers = { 'Authorization': 'Bearer ' + window.$token };
        var formData = new FormData(current[0]);
        var data = {};
        if (method === 'GET') {
            var query;
            formData.forEach((v, k) => {
                if (query) query += '&';
                query += k + '=' + v;
            });
            if (url.indexOf('?') === -1) url += '?' + query;
            else url += '&' + query;
        }
        else {
            formData.forEach((v, k) => data[k] = v);
        }
        $.ajax({
            type: method,
            url: url,
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(data),
            headers: headers,
            success: function (d) {
                result.html(`<pre class="prettyprint border-0 linenums w-100">${JSON.format(d)}</pre>`);
                prettyPrint();
            },
            error: function (e) {
                result.html(`<pre class="prettyprint border-0 linenums w-100">${JSON.format(e.responseText)}</pre>`);
                prettyPrint();
            }
        });
    },
    reset: function (current) {
        current = $(current).parents('.modal').find('form');
        current.reset();
    }
};
JSON.format = function (code) {
    if (typeof code === "object")
        code = JSON.stringify(code);
    var indent = '    ';
    var br = '\r\n';
    var index = 0;
    var getIndent = function () {
        var current = '';
        for (let i = 0; i < index; i++) {
            current += indent;
        }
        return current;
    }
    var getQuote = function (quote, start) {
        var name = quote;
        for (let i = start + 1; i < code.length; i++) {
            var current = code.charAt(i);
            if (current === '\\') {
                i++;
                name += current;
                name += code.charAt(i);
            }
            else if (current === quote) {
                name += current;
                break;
            }
            else {
                name += current;
            }
        }
        return name;
    }
    var result = '';
    var iskey = true;
    for (let i = 0; i < code.length; i++) {
        var current = code.charAt(i);
        if (current === '{') {
            iskey = true;
            result += current;
            result += br;
            index++;
        }
        else if (current === '}') {
            index--;
            result += br;
            result += getIndent();
            result += current;
        }
        else if (current === '\'' || current === '"' || current === '`') {
            var quote = getQuote(current, i);
            i += quote.length - 1;
            if (iskey) {
                result += getIndent();
                iskey = false;
            }
            result += quote;
        }
        else if (current === ',') {
            iskey = true;
            result += current;
            result += br;
        }
        else if (current === ':') {
            result += current;
            result += ' ';
        }
        else {
            result += current;
        }
    }
    return result;
}

if (typeof window.CodeMirror !== 'undefined') {
    var codeMirrors = document.querySelectorAll('.CodeMirror');
    codeMirrors.forEach(item => {
        window.CodeMirror.fromTextArea(item,
            {
                theme: 'idea',
                lineNumbers: true, //显示行号
                lineWrapping: true, //是否强制换行
                mode: item.getAttribute('.mode') || 'htmlmixed',
                extraKeys: { "Tab": "autocomplete" }
            });
    });
}