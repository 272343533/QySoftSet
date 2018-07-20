<template>
    <div class="layout-table-simple">
        <div class="toolbar">
            <el-button-group>
                <el-button type="primary" size="small"  @click="openDialog()"><i class="fa fa-edit"></i>新增</el-button>
                <el-button type="primary" size="small"><i class="fa fa-edit"></i>增加部门字段</el-button>
                <el-button type="primary" size="small"><i class="fa fa-edit"></i>增加操作人</el-button>
                <el-button type="primary" size="small"><i class="fa fa-edit"></i>增加审核人</el-button>
                <el-button type="primary" size="small"><i class="fa fa-edit"></i>增加逻辑删除</el-button>
            </el-button-group>
        </div>  
        <div class="grid">
            <el-table :data="gridData" border stripe highlight-current-row height="'100%'" @current-change="selectRow">
                <el-table-column prop="1" label="序号" width="100">
                </el-table-column>
                <el-table-column prop="2" label="名称" width="100">
                </el-table-column>
                <el-table-column prop="3" label="描述">
                </el-table-column>
                <el-table-column prop="4" label="类型" width="100">
                </el-table-column>
                <el-table-column prop="5" label="长度" width="100">
                </el-table-column>
                <el-table-column prop="6" label="小数位" width="100">
                </el-table-column>
                <el-table-column prop="7" label="非空" width="100">
                </el-table-column>
                <el-table-column prop="LoginName" label="操作" width="100">
                </el-table-column>
            </el-table>
        </div>
        <div class="pagination">
            <el-pagination @size-change="handleSizeChange"  @current-change="handleCurrentChange" :current-page="pagination.currentPage"
                :page-size="pagination.pageSize" layout="total, sizes, prev, pager, next" :total="pagination.total" :page-sizes="[20, 50, 100]">
            </el-pagination>
        </div>
        <el-dialog :close-on-click-modal="false" :close-on-press-escape="false" title="新增字段" v-model="dialogVisible" size="tiny">
            <el-form :model="dialogForm">
                <el-form-item label="序号：" :label-width="'120px'">
                    <el-input v-model="dialogForm.FNo" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="名称：" :label-width="'120px'">
                    <el-input v-model="dialogForm.FName" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="描述：" :label-width="'120px'">
                    <el-input v-model="dialogForm.FDesp" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="类型：" :label-width="'120px'">
                    <el-input v-model="dialogForm.FType" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="长度：" :label-width="'120px'">
                    <el-input v-model="dialogForm.FLength" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="小数位长度：" :label-width="'120px'">
                    <el-input v-model="dialogForm.DeciDigits" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="非空" :label-width="'120px'">
                    <el-input v-model="dialogForm.NotNull" auto-complete="off"></el-input>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button type="primary" @click="dialogSubmit()">提交</el-button>
                <el-button @click="dialogVisible = false">关闭</el-button>
            </div>
        </el-dialog>
    </div>
</template>
<script>
    import moment from 'moment';

    export default {
        data() {
            return {
                searchForm: {
                    TName: '',
                    Desp: '',
                    TType: ''
                },
                gridData: [],
                pagination: {
                    currentPage: 1,
                    pageSize: 20,
                    total: 0
                },
                dialogForm: {
                    No: '',
                    TName: '',
                    TPk: '',
                    Desp: ''
                },
                options: [{
                    value: '',
                    label: '全部'
                    }, {
                    value: '表',
                    label: '表'
                    }, {
                    value: '视图',
                    label: '视图'
                    }, {
                    value: '存储过程',
                    label: '存储过程'
                    }],
                dialogVisible: false,
                currentRow:""
            };
        },
        methods: {
            search() {
                const me = this;
                this.$ajax('get', '/api/bsfield/getallwithpaging', {
                    // 改写后端方法
                    where: JSON.stringify([{
                            key: 'TName',
                            val: me.searchForm.TName
                        }]),
                    currentPage: me.pagination.currentPage,
                    pageSize: me.pagination.pageSize
                }, function (res) {
                    me.gridData = res.data;
                    me.pagination.currentPage = res.currentPage;
                    me.pagination.pageSize = res.pageSize;
                    me.pagination.total = res.totalCount;
                });
            },
            handleSizeChange(val) {
                this.pagination.pageSize = val;
                this.search();
            },
            handleCurrentChange(val) {
                this.pagination.currentPage = val;
                this.search();
            },
            removeRow(row) {
                var me = this;
                this.$confirm('是否删除此项数据?', '警告', {
                    confirmButtonText: '删除',
                    cancelButtonText: '取消',
                    type: 'error'
                }).then(() => {
                    this.$ajax('post', '/api/bsfield/delete', {
                        idValue: row.bsT_Idssms
                    }, function (res) {
                        me.search();
                    });
                }).catch(() => {

                });

            },
            selectRow(val){
                console.log(val.bsU_Id);
                this.currentRow=val.bsU_Id;
            },
            openDialog(id) {
                const me = this;
                if (id) {
                    // 编辑根据主键请求数据并赋值form
                    console.log(id)
                    this.$ajax('post', '/api/bsfield/getone', {
                        idValue: id
                    }, function (res) {
                        console.log(res)
                        me.dialogForm = res;
                    });
                    this.dialogVisible = true;
                } else {
                    // 新增 清空form
                    this.dialogForm = {
                        No: '',
                        TName: '',
                        TPk: '',
                        Desp: '',
                        TType: '表',
                    };
                    this.dialogVisible = true;
                }
            },
            dialogSubmit() {
                var me = this;
                this.$ajax('post', '/api/bsfield/add', {
                    strJson: JSON.stringify({
                        No: me.dialogForm.No,
                        TName: me.dialogForm.TName,
                        TPk: me.dialogForm.TPk,
                        Desp: me.dialogForm.Desp,
                        TType: me.dialogForm.TType,
                    })
                }, function (res) {
                    me.dialogVisible = false;
                    me.search();
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