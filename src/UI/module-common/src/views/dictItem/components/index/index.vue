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

    <!--添加-->
    <add-page :total="total" :parent-id="parentId" :dict="dict" :visible.sync="dialog.add" @success="onAdd" />
    <!--编辑-->
    <edit-page :id="curr.id" :visible.sync="dialog.edit" @success="onEdit" />
  </nm-list>
</template>
<script>
import { mixins } from 'netmodular-ui'
import cols from './cols'
import AddPage from '../add'
import EditPage from '../edit'

const api = $api.common.dictItem

export default {
  mixins: [mixins.dialog],
  components: { AddPage, EditPage },
  data() {
    return {
      curr: { id: '' },
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
      removeAction: api.remove,
      dialog: {
        add: false,
        edit: false
      },
      total: 0
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
    add(total) {
      this.total = total
      this.dialog.add = true
    },
    edit(row) {
      this.curr = row
      this.dialog.edit = true
    },
    onAdd(model) {
      this.$emit('add-success', model)
      this.refresh()
    },
    onDelete(row) {
      this.$emit('del-success', row.id)
      this.refresh()
    },
    onEdit(model, data) {
      console.log(model)
      console.log(data)
      this.$emit('edit-success', { id: model.id, label: model.name, item: Object.assign({}, model) })
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
