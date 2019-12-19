<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
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

      <template v-slot:col-icon="{ row }">
        <nm-icon class="nm-size-20" :name="row.icon" />
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <nm-button v-bind="buttons.edit" @click="edit(row)" />
        <nm-button-delete v-bind="buttons.del" :id="row.id" :action="removeAction" @success="refresh" />
      </template>
    </nm-list>

    <!--编辑-->
    <save-page :id="curr.id" :total="total" :visible.sync="dialog.save" @success="refresh" />
  </nm-container>
</template>
<script>
import { mixins } from 'netmodular-ui'
import page from './page'
import cols from './cols'
import SavePage from '../components/save'

// 接口
const api = $api.common.dictGroup

export default {
  name: page.name,
  mixins: [mixins.list],
  components: { SavePage },
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        model: {
          /** 名称 */
          name: '',
          /** 编码 */
          code: '',
          //图标
          icon: ''
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  }
}
</script>
