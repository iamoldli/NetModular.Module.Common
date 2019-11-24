<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" clearable autofocus />
        </el-form-item>
        <el-form-item label="值：" prop="value">
          <el-input v-model="form.model.value" clearable />
        </el-form-item>
        <el-form-item label="扩展数据：" prop="extend">
          <el-input type="textarea" v-model="form.model.extend" clearable />
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

// 接口
const api = $api.common.dictItem

export default {
  mixins: [mixins.dialog],
  data() {
    return {
      form: {
        title: '添加字典数据项',
        icon: 'add',
        action: api.add,
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
          sort: ''
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
        success: this.onSuccess,
        open: this.onOpen
      }
    }
  },
  props: {
    dict: Object,
    parentId: Number,
    total: Number
  },
  methods: {
    onSuccess(id) {
      this.form.model.id = id
      this.$emit('success', { id, label: this.form.model.name, item: Object.assign({}, this.form.model) })
    },
    onOpen() {
      this.$nextTick(() => {
        this.$refs.form.reset()
        let model = this.form.model
        model.groupCode = this.dict.groupCode
        model.dictCode = this.dict.code
        model.parentId = this.parentId
        model.sort = this.total
      })
    }
  }
}
</script>
