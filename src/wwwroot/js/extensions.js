String.prototype.isClassSelector = isClassSelector;
String.prototype.isIdSelector = isIdSelector;

function isClassSelector() {
    return this.toString().charAt(0) == ".";
}
function isIdSelector() {
    return this.toString().charAt(0) == "#";
}