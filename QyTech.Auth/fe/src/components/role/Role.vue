<template class="layout-tree-edit">
  <div class="main">
    <el-row :gutter="24">
      <el-col :span="4">
                <qy-tree url="/api/bsOrganize/treeDis" v-on:emitId="clickNode"></qy-tree>
            </el-col>
      <el-col :span="18">
        <div class="grid-content bg-purple role-set">
          <div class="query-condition">
            <el-form :inline="true">
              <el-form-item>
                <el-input v-model="input.Code" placeholder="角色编码"></el-input>
              </el-form-item>
              <el-form-item>
                <el-input v-model="input.Name" placeholder="角色名称"></el-input>
              </el-form-item>
              <el-form-item>
                <el-button type="primary" icon="search" @click="search()">查询</el-button>
              </el-form-item>
            </el-form>
          </div>
          <div class="toolbar">
            <el-button-group>
              <el-button type="info" icon="edit" size="small" @click="add()">添加</el-button>
              <el-button type="info" icon=" el-icon-setting" size="small">数据项配置</el-button>
            </el-button-group>
          </div>
          <div>
            <el-table :data="tableData" border style="width:100%;overflow:auto;">
              <el-table-column prop="$id" label="序号" width="80">
              </el-table-column>
              <el-table-column prop="Code" label="角色编码" width="100">
              </el-table-column>
              <el-table-column prop="Name" label="角色名称" width="160">
              </el-table-column>
              <el-table-column prop="Desp" label="角色描述">
              </el-table-column>
              <el-table-column prop="IsSysRole" label="是否系统角色" width="160">
              </el-table-column>
              <el-table-column :context="_self" fixed="right" label="操作" width="120">
              <template scope="scope">
                  <el-button type="text" size="small" @click="add(scope.row.bsR_Id)">查看</el-button>
                  <el-button type="text" size="small" @click="deleterole(scope.row)">删除</el-button>
                  <el-button type="text"  size="small" @click="pagePermission()">页面权限</el-button>
              </template>
              </el-table-column>
            </el-table>
          </div>
          <el-pagination :current-page="5" :page-size="100" layout="total, prev, pager, next" :total="1000" style="float:right">
          </el-pagination>
        </div>
      </el-col>
    </el-row>
    <el-dialog :close-on-click-modal="false" :close-on-press-escape="false" :title="dialogTitle" v-model="dialogvisiblerole" size="tiny">
            <el-form :model="dialogrole">
                <el-form-item label="角色编码：" :label-width="'120px'">
                    <el-input v-model="dialogrole.Code" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="角色名称：" :label-width="'120px'">
                    <el-input v-model="dialogrole.Name" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="角色描述：" :label-width="'120px'">
                    <el-input v-model="dialogrole.Desp" auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="是否系统角色：" :label-width="'120px'">
                    <el-switch v-model="dialogrole.IsSysRole" on-text="是" off-text="否">
                    </el-switch>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogrole = false">关闭</el-button>
                <el-button type="primary" @click="onEditSubmit" v-if="dialogrole.$id">修改</el-button>
                <el-button type="primary" @click="onSubmit" v-else>添加</el-button>
            </div>
        </el-dialog>
        <el-dialog title="页面权限" v-model="permission">
            <el-tree :data="permdata"   ref="permdata" :props="defaultProps" :highlight-current="true" :expand-on-click-node="false" default-expand-all="default-expand-all" show-checkbox node-key="id"></el-tree>
            <div slot="footer" class="dialog-footer">
                <el-button @click="permission = false">取 消</el-button>
                <el-button type="primary" @click="">确 定</el-button>
            </div>
        </el-dialog>
  </div>
</template>
<script>
  export default {
    data() {
      return {
        input: {
          Code:'',
          Name:''
        },
                 defaultProps: {
                    children: 'items',
                    label: 'title'
                },
                permdata: [],
                permission: false,
         dialogrole: {
                    Code: '',
                    Name: '',
                    Desp: '',
                    IsSysRole: false
                },
                dialogvisiblerole: false,
         pagination: {
                    currentPage: 1,
                    pageSize: 20,
                    total: 0
                },
       tableData: [],
       dialogTitle:'',
      };
    },
     props: ['url', 'onclick',  'default-expand-all'],
        methods: {
            search() {
                const me = this;
                this.$ajax('get', '/api/bsrole/getallwithpaging', {
                    // 改写后端方法
                    where: JSON.stringify([{
                            key: 'Code',
                            val: me.input.Code
                        }, {
                            key: 'Name',
                            val: me.input.Name
                        }]),
                    currentPage: me.pagination.currentPage,
                    pageSize: me.pagination.pageSize
                }, function (res) {
                    me.tableData = res.data;
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
            deleterole(row) {
                var me = this;
                this.$confirm('是否删除此项数据?', '警告', {
                    confirmButtonText: '删除',
                    cancelButtonText: '取消',
                    type: 'error'
                }).then(() => {
                    this.$ajax('post', '/api/bsrole/delete', {
                        idValue: row.bsR_Id
                    }, function (res) {
                        me.search();
                    });
                }).catch(() => {
                });
            },
            pagePermission(id) {
               const me = this;
               this.permission = true;
                  this.$ajax('get', '/api/bsnavigation/getall', {}, function (res) {
                me.permdata = res;
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
             add(id) {
                const me = this;
                if (id) {
                    this.dialogTitle = "查看角色";
                    console.log(id)
                    this.$ajax('post', '/api/bsrole/getone', {
                        idValue: id
                    }, function (res) {
                        console.log(res)
                        me.dialogrole = res;
                    });
                    this.dialogvisiblerole = true;
                } else {
                    this.dialogTitle = "添加角色";
                    this.dialogrole = {
                        Code: '',
                        Name: '',
                        Desp: '',
                        IsSysRole: false
                    };
                    this.dialogvisiblerole = true;
                }
            },
            onSubmit() {
                var me = this;
                this.$ajax('post', '/api/bsrole/add', {
                    strJson: JSON.stringify({
                        Code: me.dialogrole.Code,
                        Name: me.dialogrole.Name,
                        Desp: me.dialogrole.Desp,
                        IsSysRole: me.dialogrole.IsSysRole,
                        bsO_Id: 'f2c52587-19c6-4dd0-9671-6d05045c1d41'
                    })
                }, function (res) {
                    me.dialogvisiblerole= false;
                    me.search();
                });
        },
            onEditSubmit() {
                var me = this;
                this.$ajax('post', '/api/bsrole/edit', {
                    strJson: JSON.stringify({
                        Code: me.dialogrole.Code,
                        Name: me.dialogrole.Name,
                        Desp: me.dialogrole.Desp,
                        IsSysRole: me.dialogrole.IsSysRole,
                        bsO_Id: 'f2c52587-19c6-4dd0-9671-6d05045c1d41'
                    })
                }, function (res) {
                    me.dialogvisiblerole= false;
                    me.search();
                });
         }
        },
        created: function () {
            this.search();
        }
    }
</script>
<style scoped>
  .el-row {
    margin-bottom: 20px;
    &:last-child {
      margin-bottom: 0;
    }
  }
  
  .el-col {
    border-radius: 4px;
    height: 100%;
  }
  
  .bg-purple-dark {
    background: #99a9bf;
  }
  
  .grid-content {
    border-radius: 4px;
    min-height: 36px;
  }
  
  .data-Oper {
    float: right;
  }
  
  .normal-operate {
    float: right;
  }
  
  .el-row {
    height: 100%;
  }
  
  .el-tree {
    height: 100%;
  }
  
  div[class="grid-content bg-purple"] {
    height: 90%;
  }
</style>