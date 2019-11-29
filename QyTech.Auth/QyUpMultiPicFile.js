function qyUpMultiPicFiles(url, subdir, okcallback, failcallback) {
    var _url = url;
    var _subdir = subdir;
    var _okCallback = okcallback;
    var _failCallback = failcallback;


    /*
          三个参数
          file：一个是文件(类型是图片格式)，
          w：一个是文件压缩的后宽度，宽度越小，字节越小
          objDiv：一个是容器或者回调函数
          photoCompress()
           */
    function photoCompress(file, w, objDiv) {
        var Comfileobj = file;
        var fread = new FileReader();
        /*开始读取指定的Blob对象或File对象中的内容. 当读取操作完成时,readyState属性的值会成为DONE,如果设置了onloadend事件处理程序,则调用之.同时,result属性中将包含一个data: URL格式的字符串以表示所读取文件的内容.*/
        fread.readAsDataURL(Comfileobj);
        fread.onload = function () {
            var re = this.result;
            canvasDataURL(re, w, objDiv)
        }
    }
    function canvasDataURL(path, obj, callback) {
        var img = new Image();
        img.src = path;
        img.onload = function () {
            var that = this;
            // 默认按比例压缩
            var w = that.width,
                h = that.height,
                scale = w / h;
            w = obj.width || w;
            h = obj.height || (w / scale);
            var quality = 0.7;  // 默认图片质量为0.7
            //生成canvas
            var canvas = document.createElement('canvas');
            var ctx = canvas.getContext('2d');
            // 创建属性节点
            var anw = document.createAttribute("width");
            anw.nodeValue = w;
            var anh = document.createAttribute("height");
            anh.nodeValue = h;
            canvas.setAttributeNode(anw);
            canvas.setAttributeNode(anh);
            ctx.drawImage(that, 0, 0, w, h);
            // 图像质量
            if (obj.quality && obj.quality <= 1 && obj.quality > 0) {
                quality = obj.quality;
            }
            // quality值越小，所绘制出的图像越模糊
            var base64 = canvas.toDataURL('image/jpeg', quality);
            // 回调函数返回base64的值
            callback(base64);
        }
    }
    /**
     * 将以base64的图片url数据转换为Blob
     * @param urlData
     *            用url方式表示的base64图片数据
     */
    function convertBase64UrlToBlob(urlData) {
        var arr = urlData.split(','), mime = arr[0].match(/:(.*?);/)[1],
            bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
        while (n--) {
            u8arr[n] = bstr.charCodeAt(n);
        }
        return new Blob([u8arr], { type: mime });
    }


    //上传文件方法
    /*
     * url: 提交地址
     * fileinput:input file 元素
     * subdir：子目录，以/结尾
     * 
     */
    this.UploadFile=function(fileinput) {

        var fileObjs = fileinput.files;
        var opcount = 0;
        for (var i = 0; i < fileObjs.length; i++) {
            var fileObj = fileObjs[i]; // js 获取文件对象
            if (fileObj.size > 1*1024 * 1024) { //大于1M，进行压缩上传
                var imgscale = (512 * 1024) / fileObj.size;

                photoCompress(fileObj, {
                    quality: imgscale
                }, function (base64Codes) {
                    //console.log("压缩后：" + base.length / 1024 + " " + base);
                    var form = new FormData(); // FormData 对象
                    form.append("subpath", _subdir)
                    var bl = convertBase64UrlToBlob(base64Codes);
                    form.append("file", bl, "f_" + Date.parse(new Date()) + random(1, 1000) + ".jpg"); // 文件对象

                    PostFileForm(_url, form);
                });
            } else { //原图上传
                var form = new FormData(); // FormData 对象
                form.append("subpath", _subdir)
                form.append("file", fileObj); // 文件对象
                //PostFileForm(_url, form);

                PostFileForm(_url,form);
            }
        }
    }

    /**
     * 产生随机整数，包含下限值，包括上限值
     * @param {Number} lower 下限
     * @param {Number} upper 上限
     * @return {Number} 返回在下限到上限之间的一个随机整数
     */
    function random(lower, upper) {
        return Math.floor(Math.random() * (upper - lower + 1)) + lower;
    }

    function PostFileForm(url, form) {
        $.ajax({
            url: url,
            type: "post",
            data: form,
            processData: false,
            contentType: false,
            success: function (res) {
                _okCallback(ret);
                //if (res) {
                //    alert("上传成功！");
                //}
                //console.log(res);
            },
            error: function (err) {
                _failCallback(err);
                //alert("网络连接失败,稍后重试", err);
            }
        });
    }

    function PostFileXhrForm(url, form) {
        xhr = new XMLHttpRequest();  // XMLHttpRequest 对象
        xhr.open("post", url, true); //post方式，url为服务器请求地址，true 该参数规定请求是否异步处理。
        xhr.onload = _okCallback; //请求完成
        xhr.onerror = _failCallback; //请求失败

        //xhr.upload.onprogress = progressFunction;//【上传进度调用方法实现】
        xhr.upload.onloadstart = function () {//上传开始执行方法
            ot = new Date().getTime();   //设置上传开始时间
            oloaded = 0;//设置上传开始时，以上传的文件大小为0
        };
        xhr.onreadystatechange = function () {
            console.log(xhr.readyState);
            console.log(xhr.status);
            if (xhr.readyState == 4 && xhr.status == 200) {
                //unban(userid, name, node, asyn);
            }
        };


        xhr.send(form); //开始上传，发送form数据
    }


   
}