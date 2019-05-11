var app = new Vue({
    el: '#app',
    data: {
        current: 3,
        navlist: [{ name: '首页', path: '/Home/Index' },
            { name: '发现', path: '' },
            { name: '创作', path: '' },
            { name: '论坛', path: '' },
            { name: '赛事', path: '' }],
        sidelist: [{ name: '编程宝典', path: '/img/1/1.png' }
        , { name: '你问我答', path: '/img/1/2.png' }
        , { name: '作品Show', path: '/img/1/3.png' }
        , { name: '站务反馈', path: '/img/1/4.jpg' }],
        sort_by: true,
        carousellist: [{ path: '/img/轮播图/1.jpg' },
            { path: '/img/轮播图/2.png' },
            { path: '/img/轮播图/3.png' },
            { path: '/img/轮播图/4.png' }
        ],
        centerDialogVisible: false,
        title: '',
        content: '',
        dynamicTags: [],
        inputVisible: false,
        tagvalue: '',
        img: false
    },
    methods: {
        showInput() {
            this.inputVisible = true;
            this.$nextTick(() => {
                this.$refs.saveTagInput.$refs.input.focus();
            });
        },
        handleInputConfirm() {
            let tag = this.tagvalue;
            if (!!tag) {
                this.dynamicTags.push(tag);
            }
            this.inputVisible = false;
            this.tagvalue = '';
        },
        handleTagClose(tag) {
            this.dynamicTags.splice(this.dynamicTags.indexOf(tag), 1);
        },
        handleCancelPublish() {
            let that = this;
            this.$confirm('此操作使得编辑内容丢失，确定离开吗？', '提示', {
                type: 'warning',
                confirmButtonText: '确定',
                cancelButtonText: '取消',
            }).then(() => {
                // 清空标签
                that.title = '';
                that.content = '';
                that.dynamicTags = [];
                that.centerDialogVisible = false;
            }).catch(() => {
                //
            })
        },
        handlePublishPost() {
            let self = this;
            //存储的是单击按钮瞬间的时间
            let time = self.formatTime(),
                p_t = self.dynamicTags.join(),
                p_l = document.getElementById('clearlove');
            if (self.title.trim() && self.content.trim()) {
                axios({
                    //post请求服务器路径 localhost:8080/Forum/PostSummery
                    method: 'post',
                    url: '/Forum/PostSummery',
                    data: {
                        sort_by: 1,
                        title: self.title,
                        content: self.content,
                        date: time,
                        tag: p_t
                    }
                }).then(res => {
                    console.log(res.status);
                    p_l.innerHTML = res.data;

                }).catch(err => {
                    console.log(err);
                })
            } else {

            }
        },
        handleSortByWhat(typenum) {
            let self = this;
            this.sort_by = (typenum == 3) ? true : false;
            let p_l = document.getElementById('clearlove');
            axios.post('/Forum/PostSummery', {
                sortby: typenum
            }).then(res => {
                p_l.innerHTML = res.data;
            })
        },
        //时间格式化函数
        formatTime() {
            let date = new Date(),
                year = date.getFullYear(),
                month = date.getMonth() + 1,
                day = date.getDay(),
                h = date.getHours(),
                m = date.getMinutes(),
                s = date.getSeconds();
            m = m < 10 ? '0' + m : m;
            s = s < 10 ? '0' + s : s;
            let now = `${year}-${month}-${day} ${h}:${m}:${s}`;
            return now;
        }
    },
    computed: {
        dis() {
            return this.dynamicTags.length > 3;
        },
        numTip() {
            let num = 4 - this.dynamicTags.length;
            return num == 0 ? `做多可以添加4个标签` : `您还可以添加${num}个标签`;
        }
    }
})