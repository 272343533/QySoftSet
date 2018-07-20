import Vue from 'vue'
import VueRouter from 'vue-router'
import Routes from './routes'
import App from './App.vue'
import ElementUI from 'element-ui'

import { $ajax } from './core/core.js'
import QyTree from './components/common/tree.vue'
import QyToolbar from './components/common/toolbar.vue'
import 'element-ui/lib/theme-default/index.css'
import './core/core.scss'

Vue.use(VueRouter)
Vue.use(ElementUI)

Vue.prototype.$ajax = $ajax

Vue.component('qy-tree', QyTree)
Vue.component('qy-toolbar', QyToolbar)

const router = new VueRouter({
  routes: Routes
})

new Vue({
  el: '#app',
  render: h => h(App),
  router
})