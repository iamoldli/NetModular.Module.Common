import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    select: root + 'Select',
    tree: root + 'Tree'
  }

  const select = params => {
    return $http.get(urls.select, params)
  }

  const tree = params => {
    return $http.get(urls.tree, params)
  }

  return {
    ...crud,
    select,
    tree
  }
}
