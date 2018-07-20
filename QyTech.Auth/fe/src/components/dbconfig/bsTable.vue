<template>
    <div class="layout-table-simple">
        <div class="condition">
            <el-form :inline="true" :model="searchForm">
                <el-form-item>
                    <el-input v-model="searchForm.TName" placeholder="名称"></el-input>
                </el-form-item>
                <el-form-item>
                    <el-input v-model="searchForm.Desp" placeholder="描述"></el-input>
                </el-form-item>
                <el-form-item>
                     <el-select v-model="searchForm.TType" placeholder="请选择">
                        <el-option
                        v-for="item in options"
                        :key="item.value"
                        :label="item.label"
                        :value="item.value">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" @click="search()"><i class="fa fa-search"></i> 查询</el-button>
                </el-form-item>
            </el-form>
        </div>
        <div class="toolbar">
            <el-button-group>
                <el-button type="primary" size="small" @click="openDialog()"><i class="fa fa-plus"></i> 新增 </el-button>
            </el-button-group>
        </div>
        <div class="grid">
            <el-table :data="gridData" border stripe height="'100%'">
                <el-table-column prop="No" label="序号" width="100">
                </el-table-column>
                <el-table-column prop="TName" label="名称" width="100">
                </el-table-column>
                <el-table-column prop="TPk" label="表主键" width="100">
                </el-table-column>
                <el-table-column prop="Desp" label="描述">
                </el-table-column>
                <el-table-column prop="TType" label="分类" width="100">
                </el-table-column>
                <el-table-column :context="_self" fixed="right" label="操作" width="200">
                    <template scope="scope">
                        <el-button size="small" type="text" @click="openDialog(scope.row.bsU_Id)">编辑</el-button>
                        <el-button size="small" type="text" @click="removeRow(scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>
        <div class="pagination">
            <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="pagination.currentPage"
                :page-size="pagination.pageSize" layout="total, sizes, prev, pager, next" :total="pagination.total" :page-sizes="[20, 50, 100]">
            </el-pagination>
        </div>
        <el-dialog :close-on-click-modal="false" :close-on-press-escape="false" title="新增表" v-model="dialogVisible" size="tiny">
            <el-form :model="dialogForm">
                <el-form-item label="序号：" :label-width="'120px'">
                    <el-input v-model="dialogForm.No" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="名称：" :label-width="'120px'">
                    <el-input v-model="dialogForm.TName" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="表主键：" :label-width="'120px'">
                    <el-input v-model="dialogForm.TPk" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="描述：" :label-width="'120px'">
                    <el-input v-model="dialogForm.Desp" auto-complete="off"></el-input>
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
                dialogVisible: false
            };
        },
        methods: {
            datetime(row, col) {
                return moment(row.RegDt).format('YYYY-MM-DD');
            },
            search() {
                const me = this;
                this.$ajax('get', '/api/bstable/getallwithpaging', {
                    // 改写后端方法
                    where: JSON.stringify([{
                            key: 'TName',
                            val: me.searchForm.TName
                        }, {
                            key: 'Desp',
                            val: me.searchForm.Desp
                        }, {
                            key: 'TType',
                            val: me.searchForm.TType
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
                    this.$ajax('post', '/api/bsuser/delete', {
                        idValue: row.bsU_Id
                    }, function (res) {
                        me.search();
                    });
                }).catch(() => {

                });

            },
            openDialog(id) {
                const me = this;
                if (id) {
                    // 编辑根据主键请求数据并赋值form
                    console.log(id)
                    this.$ajax('post', '/api/bsuser/getone', {
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
                this.$ajax('post', '/api/bsuser/add', {
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