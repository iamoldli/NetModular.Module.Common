/** 页面信息 */
const page = new (function() {
  this.title = '字典列表'
  this.icon = 'tag'
  this.name = 'common_dict'
  this.path = '/common/dict'

  // 关联权限
  this.permissions = [`${this.name}_query_get`, `${this.name}_querychildren_get`]

  this.buttons = {
    add: {
      text: '添加',
      type: 'success',
      code: `${this.name}_add`,
      icon: 'add',
      permissions: [`${this.name}_add_post`]
    },
    edit: {
      text: '编辑',
      icon: 'edit',
      type: 'text',
      code: `${this.name}_edit`,
      permissions: [`${this.name}_edit_get`, `${this.name}_update_post`]
    },
    del: {
      text: '删除',
      icon: 'delete',
      type: 'text',
      code: `${this.name}_del`,
      permissions: [`${this.name}_delete_delete`]
    },
    item: {
      text: '管理数据项',
      icon: 'edit',
      type: 'text',
      code: `${this.name}_item`,
      permissions: [`common_dictitem_query_get`, `common_dictitem_add_post`, `common_dictitem_edit_get`, `common_dictitem_update_post`, `common_dictitem_delete_delete`]
    }
  }
})()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "Common.Dict" */ './index')
}

export default page
