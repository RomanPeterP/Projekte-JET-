
window.addEventListener("DOMContentLoaded", init);


function init() {
    setBgColorOnSite();
}

function lstBgColorChange(combo) {
    setBgColorInCookie(combo, 365);
    setBgColorOnSite();
}

function setBgColorInCookie(combo, days) {
    var bgColor = combo.value;
    // 1 Tag = 24 Std = 24*60*60 = 86400 Sekunden.
    var expires = (new Date((new Date().getTime() + (days * 86400 * 1000)))).toUTCString();
    // alert(expires);
    document.cookie = "bgColor=" + bgColor + " ;expires=" + expires;
}

function setBgColorOnSite() {
    var bgColor = getCookieValue("bgColor");
    if (!bgColor) bgColor = "white";
    document.body.style.backgroundColor = bgColor;
    var lst = document.getElementById("lstBgColor");
    if (lst)
        lst.value = bgColor;
}

function getCookieValue(name) {
    var cookies = document.cookie.split("; ");
    for (var cookie of cookies) {
        const [key, value] = cookie.split("=");
        if (key === name) {
            return value;
        }
    }
    return null;
}

function deleteCookieClick()
{
    document.cookie = "bgColor=;expires=" + new Date(1970, 1, 1).toUTCString();
    setBgColorOnSite();
}

