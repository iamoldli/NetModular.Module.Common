<template>
  <nm-list ref="list" v-bind="list">
    <!--查询条件-->
    <template v-slot:querybar>
      <el-form-item label="名称：" prop="name">
        <el-input v-model="list.model.name" clearable />
      </el-form-item>
    </template>

    <!--按钮-->
    <template v-slot:querybar-buttons="{ total }">
      <nm-button text="添加" type="success" icon="add" @click="add(total)" />
    </template>

    <!--操作列-->
    <template v-slot:col-operation="{ row }">
      <nm-button text="编辑" type="text" icon="edit" @click="edit(row)" />
      <nm-button-delete type="text" icon="delete" :id="row.id" :action="removeAction" @success="onDelete(row)" />
    </template>

    <save-page :id="curr.id" :total="total" :parent-id="parentId" :dict="dict" :visible.sync="dialog.save" @success="onSave" />
  </nm-list>
</template>
<script>
import { mixins } from 'netmodular-ui'
import cols from './cols'
import SavePage from '../save'

const api = $api.common.dictItem

export default {
  mixins: [mixins.list],
  components: { SavePage },
  data() {
    return {
      list: {
        title: '数据项列表',
        cols,
        action: api.query,
        model: {
          groupCode: this.dict.groupCode,
          dictCode: this.dict.code,
          parentId: this.parentId,
          name: '',
          code: ''
        }
      },
      removeAction: api.remove
    }
  },
  props: {
    dict: Object,
    parentId: Number
  },
  methods: {
    refresh() {
      this.list.model.groupCode = this.dict.groupCode
      this.list.model.dictCode = this.dict.code
      this.list.model.parentId = this.parentId
      this.$nextTick(() => {
        this.$refs.list.refresh()
      })
    },
    onSave(isAdd, model) {
      if (isAdd) {
        this.$emit('add-success', model)
      } else {
        this.$emit('edit-success', model)
      }
      this.refresh()
    },
    onDelete(row) {
      this.$emit('del-success', row.value)
      this.refresh()
    }
  },
  watch: {
    parentId() {
      this.refresh()
    },
    dict() {
      this.refresh()
    }
  }
}
</script>
