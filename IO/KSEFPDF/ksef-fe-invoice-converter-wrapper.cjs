function setTimeout(fun) { fun(); }
function clearTimeout() { }
function log(msg) { logImpl(msg); }

var exports = {};
var module = 'ksef';
var navigator = {};
var window = { navigator: navigator, xpdfMake: {} };
var console = { log: log, warn: log, error: log };
window.window = window;

class FileReader {
	onload;
	constructor() { }
	readAsText(file) { this.onload({ target: { result: file }}); }
}