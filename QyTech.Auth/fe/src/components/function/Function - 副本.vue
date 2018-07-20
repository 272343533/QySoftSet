<template>
  <div class="main">
    <el-row :gutter="24" >
      <el-col :span="8">
        <div class="grid-content bg-purple">
          <el-card class="box-card">
            <el-tree :data="tree" :props="defaultProps"   :default-expand-all="true" :highlight-current="true" @node-click="handleNodeClick" :node-key="id"  ></el-tree>
          </el-card>
        </div>
      </el-col>
      <el-col :span="16">
        <div class="grid-content bg-purple" >

          <el-form :model="formAlignLeft" label-position="left" label-width="80px" class="layout form">

            <el-input v-model="formAlignLeft.bsN_Id" style="display:none" ></el-input>

            <el-form-item label="位置序号">
              <el-input v-model="formAlignLeft.NaviNo" id="000001" size="small"></el-input>
            </el-form-item>
            <el-form-item label="导航名称">
              <el-input v-model="formAlignLeft.NaviName" size="small"></el-input>
            </el-form-item>
            <el-form-item label="导航类别" prop="resource">
              <el-radio-group v-model="formAlignLeft.NaviType" size="small">
                <el-radio label="分组"></el-radio>
                <el-radio label="页面"></el-radio>
              </el-radio-group>
            </el-form-item>
            <el-form-item label="路由">
              <el-input v-model="formAlignLeft.Route" size="small"></el-input>
            </el-form-item>
           <el-form-item label="使用状态">
              <el-input v-model="formAlignLeft.NaviStatus" size="small"></el-input>
            </el-form-item>
            <el-form-item label="服务器地址">
              <el-input v-model="formAlignLeft.UrlServerEx" size="small"></el-input>
            </el-form-item>
            <el-form-item label="需要快捷方式">
              <el-input v-model="formAlignLeft.IsShortkey" size="small"></el-input>
            </el-form-item>
            <el-form-item label="快捷图标">
              <el-input v-model="formAlignLeft.Icon" size="small"> </el-input>
            </el-form-item>
            <el-form-item label="启用用户策略">
              <el-input v-model="formAlignLeft.UseUserPolicy" size="small"></el-input>
            </el-form-item>
            <el-form-item label="启用字段权限">
              <el-input v-model="formAlignLeft.UseRoleFields" size="small"></el-input>
            </el-form-item>
            <el-form-item class="button">
              <el-button type="primary" @click="addNode" size="small" >新增</el-button>
              <el-button type="primary" @click="onSubmit" size="small">保存</el-button>
              <el-button @click="deleteNode" size="small">删除</el-button>
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
          pId:null,
          id:null,
          name:'新增节点',
          code:'0001' ,
          type:'公司'

        },
        tree:[],
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
            if(this.data=""[i=""].id=""=id=""
              return="" i=""
              break=""
            }
          }

      },
      onSubmit=""
        console.log=""
        var="" cc="this.formAlignLeft"
        this.data.forEach=""(function=""(item="",index="",array=""
          if=""(item.id=""=cc.id=""
          {
            item.name="cc.name"
            item.code="cc.code"
            item.type="cc.type"
          }
        })
      },
      addNode=""
        this.formAlignLeft.name="null"
        this.formAlignLeft.code="null"
        this.formAlignLeft.type="null"
      },
      deleteNode=""
        var="" pos="this.filter"(this.formAlignLeft.id=""
        this.data.splice=""(pos="",1=""
      }




    },
    created:function=""
      var="" root=""
      var="" me="this"
      this.$ajax=""('get','/api/bsnavigation/treeDis',{},function=''(treeData=''){
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
<style scoped=''>
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