	import Welcome from './components/welcome/Welcome.vue'
import User from './components/User/user.vue'
import Role from './components/Role/role.vue'
import Company from './components/company/Company.vue'
import Function from './components/function/Function.vue'
import baseQuery from './components/example/baseQuery.vue'
import treeGrid from './components/example/treeGrid.vue'
import treeEdit from './components/example/treeEdit.vue'

import bsTable from './components/dbconfig/bsTable.vue'
import dbfconfig from './components/dbconfig/dbfconfig.vue'
import ffconfig from './components/function/ffconfig.vue'
import ficonfig from './components/function/ficonfig.vue'
import foconfig from './components/function/foconfig.vue'
import funconfig from './components/function/funconfig.vue'
import fqconfig from './components/function/fqconfig.vue'

const routes = [{
    path: '/welcome',
    component: Welcome
}, {
    path: '/user',
    component: User
}, {
    path: '/role',
    component: Role
}, {
    path: '/company',
    component: Company
}, {
    path: '/function',
    component: Function
}, {
    path: '/example/baseQuery',
    component: baseQuery
}, {
    path: '/example/treeGrid',
    component: treeGrid
}, {
    path: '/example/treeEdit',
    component: treeEdit
}, {
    path: '/dbconfig',
    component: bsTable
}, {
    path: '/dbfconfig',
    component: dbfconfig
}, {
    path: '/ffconfig',
    component: ffconfig
}, {
    path: '/ficonfig',
    component: ficonfig
}, {
    path: '/funconfig',
    component: funconfig
}, {
    path: '/fqconfig',
    component: fqconfig
}, {
    path: '/foconfig',
    component: foconfig
}]

export default routes