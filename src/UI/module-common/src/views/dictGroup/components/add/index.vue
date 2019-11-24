<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" clearable autofocus />
        </el-form-item>
        <el-form-item label="编码：" prop="code">
          <el-input v-model="form.model.code" clearable />
        </el-form-item>
        <el-form-item label="图标：" prop="icon">
          <nm-icon-picker v-model="form.model.icon" />
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
const api = $api.common.dictGroup

export default {
  mixins: [mixins.dialog],
  data() {
    return {
      form: {
        title: '添加字典分组',
        icon: 'add',
        action: api.add,
        width: '500px',
        model: {
          /** 名称 */
          name: '',
          /** 编码 */
          code: '',
          // 图标
          icon: '',
          /** 排序 */
          sort: 0
        },
        rules: {
          name: [{ required: true, message: '请输入名称' }],
          code: [{ required: true, message: '请输入编码' }],
          sort: [
            { required: true, message: '请输入序号' },
            { type: 'number', message: '请输入数字' }
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
    total: Number
  },
  methods: {
    onSuccess() {
      this.$emit('success')
    },
    onOpen() {
      this.$nextTick(() => {
        this.$refs.form.reset()
        this.form.model.sort = this.total
      })
    }
  }
}
</script>
