<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="分组：" prop="groupCode">
          <group-select v-model="list.model.groupCode" clearable />
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
        <el-form-item label="编码：" prop="code">
          <el-input v-model="list.model.code" clearable />
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons="{ total }">
        <nm-button v-bind="buttons.add" @click="add(total)" />
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <nm-button v-bind="buttons.item" @click="manageItem(row)" />
        <nm-button v-bind="buttons.edit" @click="edit(row)" />
        <nm-button-delete v-bind="buttons.del" :id="row.id" :action="removeAction" @success="refresh" />
      </template>
    </nm-list>

    <save-page :id="curr.id" :total="total" :visible.sync="dialog.save" @success="refresh" />
    <!--管理数据项-->
    <dict-item :dict="curr" :visible.sync="dialog.item" />
  </nm-container>
</template>
<script>
import { mixins } from 'netmodular-ui'
import page from './page'
import cols from './cols'
import GroupSelect from '../../dictGroup/components/select'
import SavePage from '../components/save'
import DictItem from '../../dictItem'

// 接口
const api = $api.common.dict

export default {
  name: page.name,
  mixins: [mixins.list],
  components: { SavePage, GroupSelect, DictItem },
  data() {
    return {
      curr: { code: '' },
      list: {
        title: '字典列表',
        cols,
        action: api.query,
        model: {
          groupCode: '',
          /** 名称 */
          name: '',
          // 编码
          code: ''
        }
      },
      removeAction: api.remove,
      buttons: page.buttons,
      dialog: {
        item: false
      }
    }
  },
  methods: {
    manageItem(row) {
      this.curr = row
      this.dialog.item = true
    }
  }
}
</script>
