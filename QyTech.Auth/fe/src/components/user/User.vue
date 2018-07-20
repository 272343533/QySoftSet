<template>
    <div class="layout-tree-grid">
        <el-row :gutter="20">
            <el-col :span="4">
                <qy-tree url="/api/bsOrganize/treeDis" v-on:emitId="clickNode" ></qy-tree>
            </el-col>
            <el-col :span="20">
                <div class="condition">
                    <el-form :inline="true" :model="searchForm">
                        <el-input v-model="searchForm.nodeId" type="hidden"></el-input>
                        <el-form-item>
                            <el-input v-model="searchForm.UserName" placeholder="用户名"></el-input>
                        </el-form-item>
                        <el-form-item>
                            <el-input v-model="searchForm.LoginName" placeholder="登录名"></el-input>
                        </el-form-item>
                        <el-form-item>
                            <el-input v-model="searchForm.NickName" placeholder="昵称"></el-input>
                        </el-form-item>
                        <el-form-item>
                            <el-button type="primary" @click="search()"><i class="el-icon-search"></i> 查询</el-button>
                        </el-form-item>
                    </el-form>
                </div>
            <div class="toolbar">
                <el-button type="primary" size="small" @click="openDialog()"><i class="el-icon-plus"></i> 新增</el-button>
            </div>
            <div class="grid">
                <el-table :data="gridData"  border stripe  style="width: 100%; height: 100%;" height="'100%'" >
                     <el-table-column prop="$id" label="序号" width="60">
                     </el-table-column>
                    <el-table-column prop="UserName" label="用户名" width="80">
                    </el-table-column>
                    <el-table-column prop="LoginName" label="登录名" width="80">
                    </el-table-column>
                    <el-table-column prop="NickName" label="昵称" width="80">
                    </el-table-column>
                    <el-table-column prop="ContactTel" label="联系电话" width="125">
                    </el-table-column>
                    <el-table-column prop="RegDt" label="注册时间" :formatter="datetime" width="115">
                    </el-table-column>
                    <el-table-column prop="AccountStatus" label="用户状态" width="70">
                    </el-table-column>
                    <el-table-column prop="IsOnline" label="是否在线" width="70">
                    </el-table-column>
                    <el-table-column prop="IsSysUser" label="是否系统账户" width="70">
                    </el-table-column>
                    <el-table-column prop="LoginDt" label="最后登录时间" :formatter="datetime" width="115">
                    </el-table-column>
                    <el-table-column :context="_self" fixed="right" label="操作" width="140">
                        <template scope="scope">
                            <el-button type="text" size="small"  @click="openDialog(scope.row.bsU_Id)">修改</el-button>
                            <el-button type="text" size="small" @click="removeRow(scope.row)">删除</el-button>
                            <el-button type="text" size="small" @click="passwordSet(scope.row)">密码</el-button>
                            <el-button type="text" size="small" @click="RoleConfig(scope.row.bsU_Id)">角色</el-button>
                            <el-button type="text" size="small" @click="openDialogOrg(scope.row.bsO_Id)">部门</el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </div>
            <div class="pagination">
                <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="pagination.currentPage"
                    :page-size="pagination.pageSize" layout="total, sizes, prev, pager, next" :total="pagination.total" :page-sizes="[20, 50, 100]">
                </el-pagination>
            </div>
        </el-col>
    </el-row>
        <el-dialog :close-on-click-modal="false" :close-on-press-escape="false" :title="dialogTitle" v-model="dialogVisible" size="tiny">
            <el-form :model="dialogForm">
                <el-form-item label="用户名：" :label-width="'120px'">
                    <el-input v-model="dialogForm.UserName" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="登录名：" :label-width="'120px'">
                    <el-input v-model="dialogForm.LoginName" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="昵称：" :label-width="'120px'">
                    <el-input v-model="dialogForm.NickName" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="联系电话：" :label-width="'120px'">
                    <el-input v-model="dialogForm.ContactTel" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="注册时间：" :label-width="'120px'">
                    <el-date-picker type="date" v-model="dialogForm.RegDt" placeholder="选择日期" width="120px">
                    </el-date-picker>
                </el-form-item>
                <el-form-item label="有效期至：" :label-width="'120px'">
                    <el-date-picker type="date" v-model="dialogForm.ValidDate" placeholder="选择日期" width="120px">
                    </el-date-picker>
                </el-form-item>
                <el-form-item label="用户状态：" :label-width="'120px'">
                    <el-input v-model="dialogForm.AccountStatus" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="是否系统账户：" :label-width="'120px'">
                    <el-switch v-model="dialogForm.IsSysUser" on-text="是" off-text="否">
                    </el-switch>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogVisible = false">关闭</el-button>
                 <el-button type="primary" @click="userEditSubmit()" v-if="dialogForm.$id">修改</el-button>
                <el-button type="primary" @click="userAddSubmit()" v-else>添加</el-button>
            </div>
        </el-dialog>
        <el-dialog title="部门功能" v-model="dialogFormOrg">
            <el-tree :data="orgdata"   ref="orgdata" :props="defaultProps" :highlight-current="true" :expand-on-click-node="false" default-expand-all="default-expand-all" show-checkbox node-key="id"></el-tree>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogFormOrg = false">取 消</el-button>
                <el-button type="primary" @click="configOrg(id)">确 定</el-button>
            </div>
        </el-dialog>
      <el-dialog title="角色配置" v-model="dialogFormRole">
            <el-tree :data="roleTree"   ref="roleTree" :props="defaultPro" :highlight-current="true" :expand-on-click-node="false" default-expand-all="default-expand-all" show-checkbox node-key="id"></el-tree>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogFormRole = false">取 消</el-button>
                <el-button type="primary" @click="configUserRoles">确 定</el-button>
            </div>
        </el-dialog>
    </div>
</template>
<script>
    import moment from 'moment';
    function transformToTreeFormat(sNodes) {
        var i, l,
            key = 'id',
            parentKey = 'pId',
            childKey = 'children';
        if (!key || key == "" || !sNodes) return [];

        if (Object.prototype.toString.apply(sNodes) === "[object Array]") {
            var r = [];
            var tmpMap = {};
            for (i = 0, l = sNodes.length; i < l; i++) {
                tmpMap[sNodes[i][key]] = sNodes[i];
            }
            for (i = 0, l = sNodes.length; i < l; i++) {
                if (tmpMap[sNodes[i][parentKey]] && sNodes[i][key] != sNodes[i][parentKey]) {
                    if (!tmpMap[sNodes[i][parentKey]][childKey])
                        tmpMap[sNodes[i][parentKey]][childKey] = [];
                    tmpMap[sNodes[i][parentKey]][childKey].push(sNodes[i]);
                } else {
                    r.push(sNodes[i]);
                }
            }
            return r;
        } else {
            return [sNodes];
        }
    }
    export default {
        data() {
            return {
                defaultPro: {
          children: 'data',
          label: 'Name',
        },
        roleTree: [],
                 defaultProps: {
                    children: 'children',
                    label: 'name'
                },
                orgdata: [],
                   dialogTitle:'',
                 RoleData1: [],
                RoleData2: [{
            yvrole: '站长',
          
          }, {
           yvrole: '测温人员',
          
          }, {
            yvrole: '大屏查看',
           
          }, {
            yvrole: '普通员工',
          
          }],
             RoleForm: {
         UserName: '',
         NickName: '',
        },
                searchForm: {
                    UserName: '',
                    LoginName: '',
                    NickName: ''
                },
                gridData: [],
                pagination: {
                    currentPage: 1,
                    pageSize: 20,
                    total: 0
                },
                dialogForm: {
                    UserName: '',
                    LoginName: '',
                    NickName: '',
                    ContactTel: '',
                    RegDt: '',
                    ValidDate: '',
                    AccountStatus: false,
                    IsSysUser: false
                },
                dialogVisible: false,
                dialogFormOrg: false,
                 dialogFormRole: false
            };
        },
         props: ['url', 'onclick',  'default-expand-all'],
        methods: {
            datetime(row, col) {
                return moment(row.RegDt).format('YYYY-MM-DD');
            },
            search() {
                const me = this;
                this.$ajax('get', '/api/bsuser/getallwithpaging', {
                    // 改写后端方法
                    where: JSON.stringify([{
                            key: 'UserName',
                            val: me.searchForm.UserName
                        }, {
                            key: 'LoginName',
                            val: me.searchForm.LoginName
                        }, {
                            key: 'NickName',
                            val: me.searchForm.NickName
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
            openDialogOrg(id) {
                const me = this;
                this.dialogFormOrg = true;
                  this.$ajax('get', '/api/bsOrganize/treeDis', {}, function (treeData) {
                me.orgdata = transformToTreeFormat(treeData);
            });
            },
             configOrg(id){
       let checkedKeys = this.$refs.orgdata.getCheckedKeys();
       alert( checkedKeys)
        // console.log(checkedKeys);
        //this.$http.get(api.SYS_SET_ROLE_RESOURCE + "?roleId=" + this.form.id + "&resourceIds="+checkedKeys.join(','))
        //   .then(res => {
        //     this.$message('修改成功');
        //     this.dialogFormOrg = false;
        //   })
          idValue: row.bsU_Id;
        bsO_Id: checkedKeys;
      },
            openDialogRole(id) {
                const me = this;
                   console.log(id)
                    me.$ajax('post', '/api/bsuser/getone', {
                        idValue: id
                    }, function (res) {
                        console.log(res)
                        me.RoleForm.UserName = res,
                         me.RoleForm.NickName = res;
                    });
                this.dialogFormRole = true;
                me.$ajax('get','/api/bsrole/getallwithpaging',{}, function (res) {
                me.roleData = res;
        })
            },
 handleRoleConfig(index, row){
        this.currentRow = row;
        this.dialogVisible = true;
        if (this.roleTree.length <= 0) {
          this.$http.get( api.TEST_DATA + "?selectChildren=true")
            .then(res => {
              this.roleTree = res.data.roleList
            })
        }
        this.$http.get(api.SYS_USER_ROLE + "?id=" + row.id)
          .then(res => {
            this.$refs.roleTree.setCheckedKeys(res.data);
          })
      },
      RoleConfig(id){
          const me = this;
           console.log(id)
           this.dialogFormRole = true;
            me.$ajax('get','/api/bsrole/getallwithpaging',{}, function (res) {
                me.roleTree = res;
         });
      },
           passwordSet(row) {
                  var me = this;
                  this.$confirm('是否确认将密码重置为初始密码?', '密码重置', {
                    confirmButtonText: '确认',
                    cancelButtonText: '取消',
                    type: 'error'
                    }).then(() => {
                    this.$ajax('post', '/api/organizations', {
                        idValue: row.bsU_Id
                    }, function () {
                      
                    });
                }).catch(() => {

                });
            },
          openDialog(id) {
                const me = this;
                if (id) {

                    this.dialogTitle = "修改用户";
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
                    this.dialogTitle = "新增用户";
                    this.dialogForm = {
                        UserName: '',
                        LoginName: '',
                        NickName: '',
                        ContactTel: '',
                        RegDt: '',
                        ValidDate: '',
                        AccountStatus: false,
                        IsSysUser: false
                    };
                    this.dialogVisible = true;
                }
            },
            userAddSubmit(id) {
                var me = this;
                this.$ajax('post', '/api/bsuser/add', {
                    strJson: JSON.stringify({
                        UserName: me.dialogForm.UserName,
                        LoginName: me.dialogForm.LoginName,
                        NickName: me.dialogForm.NickName,
                        ContactTel: me.dialogForm.ContactTel,
                        RegDt: me.dialogForm.RegDt,
                        ValidDate: me.dialogForm.ValidDate,
                        AccountStatus: me.dialogForm.AccountStatus == true ? '1' : '0',
                        IsSysUser: me.dialogForm.IsSysUser,
                        bsO_Id: 'f2c52587-19c6-4dd0-9671-6d05045c1d41'
                    })
                }, function (res) {
                    me.dialogVisible = false;
                    me.search();
                });
            },
             userEditSubmit(id) {
                var me = this;
                this.$ajax('post', '/api/bsuser/edit', {
                    strJson: JSON.stringify({
                        UserName: me.dialogForm.UserName,
                        LoginName: me.dialogForm.LoginName,
                        NickName: me.dialogForm.NickName,
                        ContactTel: me.dialogForm.ContactTel,
                        RegDt: me.dialogForm.RegDt,
                        ValidDate: me.dialogForm.ValidDate,
                        AccountStatus: me.dialogForm.AccountStatus == true ? '1' : '0',
                        IsSysUser: me.dialogForm.IsSysUser,
                        bsO_Id: 'f2c52587-19c6-4dd0-9671-6d05045c1d41'
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