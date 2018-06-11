//日期格式化new Date().format('yyyy-MM-dd hh:mm:ss')
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(), //day
        "h+": this.getHours(), //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
    (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
        RegExp.$1.length == 1 ? o[k] :
        ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}
//金额增加千分号
function formatPrice(val, row) {
    var parts = val.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return "<span >￥&nbsp;" + parts.join(".") + "&nbsp;</span>";

}

//将/Date(13925635263)/格式化
function formatDate(val, row) {
    return new Date(parseInt(val.replace(/\D/igm, ""))).format('yyyy-MM-dd hh:mm');
}