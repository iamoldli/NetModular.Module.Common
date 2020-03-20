<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="名称：" prop="name">
          <el-input ref="name" v-model="form.model.name" clearable />
        </el-form-item>
        <el-form-item label="值：" prop="value">
          <el-input v-model="form.model.value" clearable />
        </el-form-item>
        <el-form-item label="扩展数据：" prop="extend">
          <extend-editor v-model="form.model.extend" />
        </el-form-item>
        <el-form-item label="图标：" prop="icon">
          <nm-icon-picker v-model="form.model.icon" clearable />
        </el-form-item>
        <el-form-item label="排序：" prop="sort">
          <el-input type="number" v-model.number="form.model.sort" clearable />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'
import ExtendEditor from '../extend-editor'

// 接口
const { add, edit, update } = $api.common.dictItem

export default {
  mixins: [mixins.formSave],
  components: { ExtendEditor },
  data() {
    return {
      title: '字典数据项',
      actions: { add, edit, update },
      form: {
        width: '650px',
        model: {
          groupCode: '',
          dictCode: '',
          parentId: 0,
          /** 名称 */
          name: '',
          /** 值 */
          value: '',
          extend: '',
          icon: '',
          /** 排序 */
          sort: 0
        },
        rules: {
          groupCode: [{ required: true, message: '请选择分组' }],
          dictCode: [{ required: true, message: '请选择字典' }],
          name: [{ required: true, message: '请输入名称' }],
          value: [{ required: true, message: '请输入值' }],
          sort: [
            { required: true, message: '请输入序号' },
            { type: 'number', message: '序号必须为数字' }
          ]
        }
      },
      on: {
        reset: this.onReset
      }
    }
  },
  props: {
    dict: Object,
    parentId: Number,
    total: Number
  },
  methods: {
    onSuccess(newId) {
      if (this.isAdd_) {
        this.form.model.id = newId
      }
      const { id, name } = this.form.model
      this.$emit('success', this.isAdd_, { id, label: name, item: Object.assign({}, this.form.model) })
    },
    onReset() {
      if (this.isAdd_) {
        let model = this.form.model
        model.groupCode = this.dict.groupCode
        model.dictCode = this.dict.code
        model.parentId = this.parentId
        model.sort = this.total
      }
      this.$refs.name.focus()
    }
  }
}
</script>
