<template>
  <div class="layout-tree-edit">
    <el-row :gutter="24" >
      <el-col :span="6"><div class="grid-content bg-purple">
      <qy-tree url="/api/bsOrganize/treeDis" v-on:emitId="clickNode" v-on:emitBtnAdd="addNode" v-on:emitBtnDel="delNode" btnEdit="true" default-expand-all="true" ></qy-tree>
        </div>
      </el-col>
      <el-col :span="18"><div class="grid-content bg-purple" >
        <el-form :model="formAlignLeft" label-position="left" label-width="80px" class="layout form">
          <el-input v-model="formAlignLeft.id" style="display:none" ></el-input>
          <el-form-item label="上级节点" size="small">
            <el-select v-model="value" placeholder="请选择" size="small">
              <el-option v-for="item in options" :label="item.label" :value="formAlignLeft.pId">
              </el-option>
            </el-select>
          </el-form-item>
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
              <el-radio label="供热站"></el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item label="机构描述">
            <el-input v-model="formAlignLeft.desp" size="small"></el-input>
          </el-form-item>
          <el-form-item label="负责人">
            <el-input v-model="formAlignLeft.principal" size="small"></el-input>
          </el-form-item>
            <el-form-item label="联系人">
            <el-input v-model="formAlignLeft.contactName" size="small"></el-input>
          </el-form-item>
          <el-form-item label="联系电话">
            <el-input v-model="formAlignLeft.tel" size="small"></el-input>
          </el-form-item>
          <el-form-item label="联系邮箱">
            <el-input v-model="formAlignLeft.email" size="small"></el-input>
          </el-form-item>
           <el-form-item label="通讯地址">
            <el-input v-model="formAlignLeft.address" size="small"></el-input>
          </el-form-item>
           <el-form-item label="邮政编码">
            <el-input v-model="formAlignLeft.post" size="small"></el-input>
          </el-form-item>
          <el-form-item class="button">
            <el-button type="primary" @click="onSubmit" size="small">保存</el-button>
          </el-form-item>
        </el-form>
        </div>
        </el-col>
    </el-row>
  </div>
</template>
<script>
    
    
  export default {
    data() {
      return {
        formAlignLeft:{
          id:null,
          pId:null,
          name:'新增节点',
          code:'0001' ,
          type:'公司',
          desp:null,
	        principal:null,
	        contactName:null,
	        tel:null,
	        email:null,
	        address:null,
	        post:null,
        },
        tree:[],
        currentNode: {},
        defaultProps: {
          children: 'children',
          label: 'name'
        }
      };
    },
    methods: {
      handleNodeClick(data,node,element) {
        console.log(node.data.code);
        var dd=node;
        this.formAlignLeft.name=node.label;
        this.formAlignLeft.id=node.data.id;
        this.formAlignLeft.type=node.data.type;
        this.formAlignLeft.code=node.data.code;
      },
      filter(id){
        console.log(this.data[0]);
          for(let i=0;i<this.data.length;i++)
          {
            if(this.data[i].id==id){
              return i;
              break;
            }
          }

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
                    me.formAlignLeft.principal=res. Principal;
                    me.formAlignLeft.contactName=res.ContactName;
                    me.formAlignLeft.tel=res.Tel;
                    me.formAlignLeft.email=res.Email;
                    me.formAlignLeft.address=res.Address;
                     me.formAlignLeft.post=res.Post;
                    //this.search();
                   
                });
            },
       onSubmit() {
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
      addNode(pNode) {
                var me=this
                console.log(pNode)
                me.formAlignLeft.bsO_Id=''
                me.formAlignLeft.pId=pNode.id
                me.formAlignLeft.name=''
                me.formAlignLeft.code=''
                me.formAlignLeft.type=''
                me.formAlignLeft.desp=''
                me.formAlignLeft.principal=''
                me.formAlignLeft.contactName=''
                me.formAlignLeft.tel=''
                me.formAlignLeft.email=''
                me.formAlignLeft.address=''
                me.formAlignLeft.post=''
            },
          delNode(node){
                this.$ajax('post', '/api/bsOrganize/Delete', {
                    idValue: node.id
                } , function (res) {
                   this.search();
                });
            },
            search() {
                var me = this;
                this.$ajax('get', '/api/bsOrganize/treeDis', {
                }, function (res) {
                    me.gridData = res.data;
                    me.pagination.currentPage = res.currentPage;
                    me.pagination.pageSize = res.pageSize;
                    me.pagination.total = res.totalCount;
                });
            },
    },
    created:function(){
      var root=[];
      var me=this;
      this.$ajax('get','/api/bsOrganize/treeDis',{},function(treeData){
        //console.log(treeData);
        for ( var i = 0; i < treeData.length; i++)
        {
            var ri = treeData[i];
            ri.text = ri.name;
            for ( var j = 0; j < treeData.length; j++)
            {
                treeData[j].leaf = true;
                for ( var k = 0; k < treeData.length; k++)
                {
                    if (treeData[k].pId == treeData[j].id&&k!=j)
                    {
                        treeData[j].leaf = false;
                        break;
                    }
                }
            }            
            if (ri.pId != ri.id)
            {
                for ( var j = 0; j < treeData.length; j++)
                {
                    var rj = treeData[j];
                    if (rj.id == ri.pId)
                    {
                        rj.children = !rj.children ? [] : rj.children;
                        rj.children.push (ri);
                        break;
                    }
                }
            }
             
            if (ri.pId == ri.id||ri.pId==null||ri.pId=="null")
            {
                root.push (ri);
            }

          }
          me.tree=root;
      });

    }
  };
</script>
<style scoped>
    .el-row {
    margin-bottom: 20px;
    &:last-child {
      margin-bottom: 20px;
    }
  }
  .el-col {
    border-radius: 4px;
    height:100%;
  }
  .el-col>*:first-child{
    height:95%;
  }
  .bg-purple {
    background: #F5F5F5;
  }
  .el-form{
    padding:20px;
    margin-left:25%;
  }
  .el-input{
    width:50%;
  }
    .grid-content {
    border-radius: 4px;
    min-height: 36px;
  }
  .el-tree{
    height:100%;
    border:0px solid;
  }
  .el-col:first-child>div[class="grid-content bg-purple"]{
    height:95%;
  }
  div[class="grid-content bg-purple"]{
    height:auto;
    position:relative;
  }
  .el-row{
    height:95%;
  }
  .el-select{
    width:50%;
  }
  .form>*{
    height:22px;
  }
  .button{
    margin-left:-8%;
    margin-top:30px;
  }
  .box-card{
    height:100%;
  }
</style>