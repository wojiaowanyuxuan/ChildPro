$(function () {
    //文本框失去焦点后
    $('form :input').blur(function(){
        var $parent = $(this).parent();
        $parent.find(".tips").remove();
        //验证用户名
        if( $(this).is('#phone') ){
            if( this.value=="" || this.value.length != 11 ){
                var errorMsg = '请正确输入！';
                $parent.append('<span class="tips onError">'+errorMsg+'</span>');
            }else{
                var okMsg = '输入正确！';
                $parent.append('<span class="tips onSuccess">'+okMsg+'</span>');
            }
        }
        //验证名字
        if( $(this).is('#name') ){
            if( this.value=="" || ( this.value!="" && !/^[\u4E00-\u9FA5\uf900-\ufa2d·s]{2,20}$/.test(this.value) ) ){
                var errorMsg = '请正确输入！';
                $parent.append('<span class="tips onError">'+errorMsg+'</span>');
            }else{
                var okMsg = '输入正确！';
                $parent.append('<span class="tips onSuccess">'+okMsg+'</span>');
            }
        }
    }).keyup(function(){
        $(this).triggerHandler("blur");
    }).focus(function(){
        $(this).triggerHandler("blur");
    });//end blur


    //提交，最终验证。
    $('#send').click(function(){
        $("form :input.form-control").trigger('blur');
        var numError = $('form .onError').length;
        if(numError){
            return false;
        }
        alert("领取成功！课程信息将会以短发形式发送给您！");
    });

    //重置
    $('#res').click(function(){
        $(".formtips").remove();
    });
})