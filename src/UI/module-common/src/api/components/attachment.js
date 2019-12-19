import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    upload: root + 'Upload',
    download: root + 'Download/'
  }

  const getUploadUrl = () => {
    return $http.axios.defaults.baseURL + urls.upload
  }
  // 下载
  const download = id => {
    return $http.download(urls.download + id)
  }

  // 预览
  const preview = id => {
    return $http.preview(urls.download + id)
  }

  return {
    ...crud,
    getUploadUrl,
    download,
    preview
  }
}
