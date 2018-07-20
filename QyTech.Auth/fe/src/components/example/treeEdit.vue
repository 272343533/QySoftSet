<template>
    <div class="layout-tree-edit">
        <el-row :gutter="20">
            <el-col :span="5">
                <qy-tree url="/api/bsOrganize/treeDis" v-on:emitId="clickNode" @emitBtnAdd="addNode" v-on:emitBtnDel="delNode" btnEdit="true" default-expand-all="true"></qy-tree>
            </el-col>
            <el-col :span="10" :offset="4">
                <el-form :model="formAlignLeft" label-position="left" label-width="80px" class="layout form">
                    <el-input v-model="formAlignLeft.bsO_Id" style="display:none"></el-input>
                    <el-input v-model="formAlignLeft.bsS_Id" style="display:none"></el-input>
                    <el-input v-model="formAlignLeft.pId" style="display:none"></el-input>
                    <!--<el-form-item label="上级节点" size="small">
                        <el-select v-model="value" placeholder="请选择" size="small">
                            <el-option v-for="item in options" :label="item.label" :value="formAlignLeft.pId">
                            </el-option>
                        </el-select>
                    </el-form-item>-->
                    <el-form-item label="机构名称">
                        <el-input v-model="formAlignLeft.name" size="small"></el-input>
                    </el-form-item>
                    <el-form-item label="机构代码">
                        <el-input v-model="formAlignLeft.code" id="000001" size="small"></el-input>
                    </el-form-item>
                    <el-form-item label="机构类别" prop="resource">
                        <el-radio-group v-model="formAlignLeft.type" size="small">
                            <el-radio label="公司"></el-radio>
                            <el-radio label="部门"></el-radio>
                            <el-radio label="其它"></el-radio>
                        </el-radio-group>
                    </el-form-item>
                    <el-form-item label="机构描述">
                        <el-input v-model="formAlignLeft.desp" size="small"></el-input>
                    </el-form-item>
                    <!--<el-form-item label="机构图标">
                        <el-input v-model="formAlignLeft.region" size="small"> </el-input>
                    </el-form-item>
                    <el-form-item label="负责人">
                        <el-input v-model="formAlignLeft.type" size="small"></el-input>
                    </el-form-item>
                    <el-form-item label="联系人">
                        <el-input v-model="formAlignLeft.name" size="small"></el-input>
                    </el-form-item>
                    <el-form-item label="联系电话">
                        <el-input v-model="formAlignLeft.region" size="small"></el-input>
                    </el-form-item>
                    <el-form-item label="联系邮箱">
                        <el-input v-model="formAlignLeft.type" size="small"></el-input>
                    </el-form-item>-->
                    <el-form-item class="button">
                        <el-button type="primary" @click="modalSubmit()" size="middle">保存</el-button>
                    </el-form-item>
                </el-form>
            </el-col>
        </el-row>
    </div>
</template>
<script>
    export default {
        data() {
            return {
                data: [],
                condition: {
                    UserName: '',
                    LoginName: '',
                    NickName: '',
                    nodeId: ''
                },
                fieldData: [],
                gridData: [],
                pagination: {
                    currentPage: 1,
                    pageSize: 20,
                    total: 0
                },
                dialogFormVisible: false,
                form: {
                    UserName: '',
                    LoginName: '',
                    NickName: '',
                    ContactTel: '',
                    RegDt: '',
                    ValidDate: '',
                    AccountStatus: false,
                    IsSysUser: false
                },
                formLabelWidth: '120px',
                formAlignLeft: {
                    pId: null,
                    id: null,
                    name: '新增节点',
                    code: '9999',
                    type: '其它'

                },
            };
        },
        methods: {
            search() {
                //qy_tree.search()
            },
            handleSizeChange(val) {
                this.pagination.pageSize = val;
                this.search();
            },
            handleCurrentChange(val) {
                this.pagination.currentPage = val;
                this.search();
            },
            openModal(flag) {
                if (flag === 'add') {
                    // 新增 清空form
                    this.dialogFormVisible = true;
                } else {
                    // 编辑根据主键请求数据并赋值form
                    console.log(flag)
                    this.dialogFormVisible = true;
                }
            },
            modalSubmit() {
                var me = this;
                console.log(this.form)
                this.$ajax('post', '/api/bsOrganize/save', {
                   strJson: JSON.stringify({
                        Name: me.formAlignLeft.name,
                        Desp: me.formAlignLeft.desp,
                        bsO_Id: me.formAlignLeft.bsO_Id,
                        bsS_Id: me.formAlignLeft.bsS_Id,
                        pId: me.formAlignLeft.pId,
                        OrgType: me.formAlignLeft.type,
                        Code: me.formAlignLeft.code
                    })
                }, function (res) {
                   me.search();
                });
            },
            clickNode(node) {
                var me=this;
                this.$ajax('post', '/api/bsOrganize/getone', {
                    idValue: node.id
                }, function (res) {
                    console.log(res);
                    me.formAlignLeft.bsO_Id=res.bsO_Id;
                    me.formAlignLeft.bsS_Id=res.bsS_Id;
                    me.formAlignLeft.name=res.Name;
                    me.formAlignLeft.desp=res.Desp;
                    me.formAlignLeft.pId=res.pId;
                    me.formAlignLeft.type=res.OrgType;
                    me.formAlignLeft.code=res.Code;
                    //this.search();
                });
            },
            addNode(pNode) {
                var me=this
                console.log(pNode)
                me.formAlignLeft.bsO_Id=''
                me.formAlignLeft.pId=pNode.id
                me.formAlignLeft.name=''
                me.formAlignLeft.desp=''
                me.formAlignLeft.type='其它'
                me.formAlignLeft.code=''
            },
            delNode(node){
                this.$ajax('post', '/api/bsOrganize/Delete', {
                    idValue: node.id
                } , function (res) {
                   this.search();
                });
            }
        },
        created: function () {
            this.search();
        }
    }
</script>
<style lang="sass" scoped>
</style>